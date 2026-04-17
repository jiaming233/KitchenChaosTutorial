using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KitchenObjectHolder
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private float moveSpeed = 7;

    [SerializeField]
    private float rotSpeed = 10;

    private bool isWalking = false;
    public bool IsWalking
    {
        get { return isActiveAndEnabled && isWalking; }
    }

    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask counterLayerMask;

    private BaseCounter selectedCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnOperateAction += GameInput_OnOperateAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    private void GameInput_OnOperateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Operate(this);
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 direction = gameInput.GetMovementDirectionNormalized();

        isWalking = direction != Vector3.zero;

        transform.position += moveSpeed * Time.deltaTime * direction;

        if (isWalking)
        {
            //transform.forward = direction;
            //对向量球形插值
            transform.forward = Vector3.Slerp(transform.forward, direction, rotSpeed * Time.deltaTime);
        }
    }

    private void HandleInteraction()
    {
        //射线检测
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 2f, counterLayerMask))
        {
            //print(hitInfo.collider.gameObject);

            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
            {
                //clearCounter.Interact();
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter counter)
    {
        if(counter != selectedCounter)
        {
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();
            selectedCounter = counter;
        }
    }
}

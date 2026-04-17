using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickup;

    [SerializeField] private Transform holdPoint;

    private KitchenObject kitchenObject;

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// 放下
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;

        if(this is BaseCounter)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this is Player)
        {
            OnPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetHoldPoint()
    {
        return holdPoint;
    }

    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (!sourceHolder.HasKitchenObject())
        {
            Debug.LogWarning("源柜台没有食材");
            return;
        }
        if (targetHolder.HasKitchenObject())
        {
            Debug.LogWarning("目标柜台已有食材");
            return;
        }
        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
    }


    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        SetKitchenObject(kitchenObject);
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

    public void DestroyKitchenObject()
    {
        if(kitchenObject != null)
        {
            Destroy(kitchenObject.gameObject);
        }
        ClearKitchenObject();
    }

    public static void ClearStaticData()
    {
        OnDrop = null;
        OnPickup = null;
    }
}

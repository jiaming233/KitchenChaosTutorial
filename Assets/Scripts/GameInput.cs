using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private const string GAMEINPUT_BINDING = "GameInputBindings";

    private GameControl gameControl;

    public event EventHandler OnInteractAction;
    public event EventHandler OnOperateAction;
    public event EventHandler OnPauseAction;

    public enum BindingType
    {
        Up,
        Down,
        Right,
        Left,
        Interact,
        Operate,
        Pause
    }

    private void Awake()
    {
        Instance = this;

        gameControl = new GameControl();
        if (PlayerPrefs.HasKey(GAMEINPUT_BINDING))
        {
            gameControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString(GAMEINPUT_BINDING));
        }
        //启用
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_Performed;
        gameControl.Player.Pause.performed += Pause_Performed;
    }

    //private void Start()
    //{
    //    Debug.Log(gameControl.Player.Move.bindings.Count);
    //    for (int i = 0; i < gameControl.Player.Move.bindings.Count; i++)
    //    {
    //        Debug.Log(gameControl.Player.Move.bindings[i].ToDisplayString());
    //    }
    //}

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate_Performed(InputAction.CallbackContext context)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        ////Old Input System
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0, vertical);

        Vector2 inputVector = gameControl.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);

        //归一化
        direction = direction.normalized;
        return direction;
    }

    private void OnDestroy()
    {
        if (gameControl != null)
        {
            gameControl.Player.Interact.performed -= Interact_Performed;
            gameControl.Player.Operate.performed -= Operate_Performed;
            gameControl.Player.Pause.performed -= Pause_Performed;
            gameControl.Dispose();
        }
    }

    public string GetBindingDisplayString(BindingType bindingType)
    {
        switch (bindingType)
        {
            case BindingType.Up:
                return gameControl.Player.Move.bindings[1].ToDisplayString();
            case BindingType.Down:
                return gameControl.Player.Move.bindings[2].ToDisplayString();
            case BindingType.Left:
                return gameControl.Player.Move.bindings[3].ToDisplayString();
            case BindingType.Right:
                return gameControl.Player.Move.bindings[4].ToDisplayString();
            case BindingType.Interact:
                return gameControl.Player.Interact.bindings[0].ToDisplayString();
            case BindingType.Operate:
                return gameControl.Player.Operate.bindings[0].ToDisplayString();
            case BindingType.Pause:
                return gameControl.Player.Pause.bindings[0].ToDisplayString();
        }
        return string.Empty;
    }

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void Rebinding(BindingType bindingType, Action onComplete = null)
    {
        if (rebindingOperation != null)
        {
            rebindingOperation.Dispose();
            rebindingOperation = null;
        }

        InputAction action = null;
        int bindingIndex = 0;

        switch (bindingType)
        {
            case BindingType.Up:
                action = gameControl.Player.Move;
                bindingIndex = 1;
                break;
            case BindingType.Down:
                action = gameControl.Player.Move;
                bindingIndex = 2;
                break;
            case BindingType.Left:
                action = gameControl.Player.Move;
                bindingIndex = 3;
                break;
            case BindingType.Right:
                action = gameControl.Player.Move;
                bindingIndex = 4;
                break;
            case BindingType.Interact:
                action = gameControl.Player.Interact;
                break;
            case BindingType.Operate:
                action = gameControl.Player.Operate;
                break;
            case BindingType.Pause:
                action = gameControl.Player.Pause;
                break;

        }

        if (action != null)
        {
            Debug.LogError("开始绑定");
            gameControl.Player.Disable();
            rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)
                .WithControlsExcluding("Mouse")
                .OnComplete(callback =>
            {
                Debug.LogError($"绑定完成 {callback.action.bindings[bindingIndex].path};{callback.action.bindings[bindingIndex].overridePath}");
                callback.Dispose();
                gameControl.Player.Enable();
                onComplete?.Invoke();

                PlayerPrefs.SetString(GAMEINPUT_BINDING, gameControl.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            });
            rebindingOperation.Start();
        }
    }
}

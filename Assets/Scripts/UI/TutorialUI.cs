using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;

    [SerializeField] private TextMeshProUGUI upKey;
    [SerializeField] private TextMeshProUGUI downKey;
    [SerializeField] private TextMeshProUGUI leftKey;
    [SerializeField] private TextMeshProUGUI rightKey;
    [SerializeField] private TextMeshProUGUI interactKey;
    [SerializeField] private TextMeshProUGUI operateKey;
    [SerializeField] private TextMeshProUGUI pauseKey;

    private void Start()
    {
        Hide();

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Show();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            GameManager.Instance.TurnToCountDownToStart();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        UpdateVisual();
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    private void UpdateVisual()
    {
        upKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operateKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        pauseKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
}

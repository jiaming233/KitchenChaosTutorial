using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public static SettingUI Instance { get; private set; }

    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private Button upKeyButton;
    [SerializeField] private Button downKeyButton;
    [SerializeField] private Button leftKeyButton;
    [SerializeField] private Button rightKeyButton;
    [SerializeField] private Button interactKeyButton;
    [SerializeField] private Button operateKeyButton;
    [SerializeField] private Button pauseKeyButton;
    private TextMeshProUGUI upKeyButtonText;
    private TextMeshProUGUI downKeyButtonText;
    private TextMeshProUGUI leftKeyButtonText;
    private TextMeshProUGUI rightKeyButtonText;
    private TextMeshProUGUI interactKeyButtonText;
    private TextMeshProUGUI operateKeyButtonText;
    private TextMeshProUGUI pauseKeyButtonText;

    [SerializeField] private Button closeButton;

    private TextMeshProUGUI soundText;
    private TextMeshProUGUI musicText;

    [SerializeField] private GameObject rebindingHint;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();

        soundText = soundButton.GetComponentInChildren<TextMeshProUGUI>();
    
        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicText = musicButton.GetComponentInChildren<TextMeshProUGUI>();
      
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        upKeyButtonText = upKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        downKeyButtonText = downKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        leftKeyButtonText = leftKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        rightKeyButtonText = rightKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        interactKeyButtonText = interactKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        operateKeyButtonText = operateKeyButton.GetComponentInChildren<TextMeshProUGUI>();
        pauseKeyButtonText = pauseKeyButton.GetComponentInChildren<TextMeshProUGUI>();

        upKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Up));
        downKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Down));
        leftKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Left));
        rightKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Right));
        interactKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Interact));
        operateKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Operate));
        pauseKeyButton.onClick.AddListener(() => Rebinding(GameInput.BindingType.Pause));

        UpdateVisual();
    }

    private void GameManager_OnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    public void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    public bool IsShow()
    {
        return uiParent.activeSelf;
    }

    private void UpdateVisual()
    {
        soundText.text = $"音效大小:{SoundManager.Instance.GetVolume()}";
        musicText.text = $"音乐大小:{MusicManager.Instance.GetVolume()}";

        upKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operateKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        pauseKeyButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }

    private void Rebinding(GameInput.BindingType bindingType)
    {
        rebindingHint.SetActive(true);
        GameInput.Instance.Rebinding(bindingType, () =>
        {
            rebindingHint.SetActive(false);
            UpdateVisual();
        });
    }
}

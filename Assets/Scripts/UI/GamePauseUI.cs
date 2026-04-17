using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button menuButton;

    // Start is called before the first frame update
    void Start()
    {
        Hide();

        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResume += GameManager_OnGameResume;

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGame();
        });
        settingButton.onClick.AddListener(() =>
        {
            SettingUI.Instance.Show();
        });
        menuButton.onClick.AddListener(() =>
        {
            //GameManager.Instance.ToggleGame();
            Loader.Load((int)Loader.Scene.GameMenuScene);
        });
    }

    private void GameManager_OnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
        GameManager.Instance.OnGameResume -= GameManager_OnGameResume;
    }
}

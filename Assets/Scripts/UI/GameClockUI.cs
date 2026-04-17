using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;

    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
            timeText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    public void Show()
    {
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }

}

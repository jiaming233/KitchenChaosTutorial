using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI numberText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            numberText.text = OrderManager.Instance.GetSuccessOrderCount().ToString();
            uiParent.SetActive(true);
        }
        else
        {
            uiParent.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
}

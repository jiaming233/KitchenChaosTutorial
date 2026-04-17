using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateProgress(float progress)
    {
        Show();
        progressBarImage.fillAmount = progress;

        if(progress == 1)
        {
            Invoke("Hide", 0.5f);
        }
    }
}

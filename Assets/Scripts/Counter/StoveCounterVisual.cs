using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject sizzlingPracticles;
    [SerializeField] private GameObject stoveOnVisual;

    public void ShowStoveEffect()
    {
        sizzlingPracticles.SetActive(true);
        stoveOnVisual.SetActive(true);
    }

    public void HideStoveEffect()
    {
        sizzlingPracticles.SetActive(false);
        stoveOnVisual.SetActive(false);
    }
}

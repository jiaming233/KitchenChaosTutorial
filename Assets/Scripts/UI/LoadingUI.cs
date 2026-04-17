using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dotText;

    [SerializeField] private float dotRate = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DotAnimation());
    }

    private IEnumerator DotAnimation()
    {
        while (dotText != null)
        {
            dotText.text = ".";
            yield return new WaitForSeconds(dotRate);
            dotText.text = "..";
            yield return new WaitForSeconds(dotRate);
            dotText.text = "...";
            yield return new WaitForSeconds(dotRate);
            dotText.text = "....";
            yield return new WaitForSeconds(dotRate);
            dotText.text = ".....";
            yield return new WaitForSeconds(dotRate);
            dotText.text = "......";
            yield return new WaitForSeconds(dotRate);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

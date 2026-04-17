using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    private const string IS_SHAKE = "IsShake";

    [SerializeField] private TextMeshProUGUI numberText;
    
    private Animator anim;

    private int preNumber;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountingDown())
        {
            numberText.gameObject.SetActive(true);
        }
        else
        {
            numberText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsCountingDown())
        {
            int number = Mathf.CeilToInt(GameManager.Instance.GetCountDownTimer());
            numberText.text = number.ToString();
            if(number != preNumber)
            {
                anim.SetTrigger(IS_SHAKE);
                SoundManager.Instance.PlayCountdownSound();
                preNumber = number;
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
}

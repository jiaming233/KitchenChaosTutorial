using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class StoveCounter : BaseCounter
{
    public enum StoveState
    {
        Idle,
        Frying,
        Burning
    }

    private const string IS_FLICKER = "IsFlicker";

    [SerializeField] private FryingRecipeListSO fryingRecipeListSO;
    [SerializeField] private FryingRecipeListSO burningRecipeListSO;

    private StoveCounterVisual stoveCounterVisual;

    [SerializeField] private ProgressBarUI progressBarUI;
    private Animator progressBarUIAnim;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private AudioSource sound;

    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0;
    private StoveState state = StoveState.Idle;

    private bool isWarning;
    private float warningSoundRate = 0.2f;
    private float warningSoundTimer;

    private void Start()
    {
        stoveCounterVisual = GetComponentInChildren<StoveCounterVisual>();
        progressBarUIAnim = progressBarUI.GetComponent<Animator>();
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (fryingRecipeListSO.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipe);
                }
                else if(burningRecipeListSO.TryGetFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
                {
                    TransferKitchenObject(player, this);
                    StartBurning(burningRecipe);
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                TransferKitchenObject(this, player);
                TurnToIdle();
            }
        }
    }

    private void Update()
    {
        switch(state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;

                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);

                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);

                    if(burningRecipeListSO.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
                    {
                        StartBurning(burningRecipe);
                    }
                }
                break;
            case StoveState.Burning:
                fryingTimer += Time.deltaTime;
                float burnProgress = fryingTimer / fryingRecipe.fryingTime;
                progressBarUI.UpdateProgress(burnProgress);
                if(burnProgress > 0.5f)
                {
                    StartWarning();
                }
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);

                    TurnToIdle();
                }
                break;
        }

        if (isWarning)
        {
            warningSoundTimer += Time.deltaTime;
            if(warningSoundTimer > warningSoundRate)
            {
                warningSoundTimer = 0;
                SoundManager.Instance.PlayWarningSound();
            }
        }
    }

    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        stoveCounterVisual.ShowStoveEffect();
        sound.Play();
    }

    private void StartBurning(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;
        stoveCounterVisual.ShowStoveEffect();
        sound.Play();
    }

    private void TurnToIdle()
    {
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        progressBarUI.Hide();
        sound.Pause();
        StopWarning();
    }

    private void StartWarning()
    {
        if (isWarning)
            return;
        isWarning = true;   
        warningUI.SetActive(true);
        progressBarUIAnim.SetBool(IS_FLICKER, true);
    }

    private void StopWarning()
    {
        warningUI.SetActive(false);
        progressBarUIAnim.SetBool(IS_FLICKER, false);
        isWarning = false;
    }
}

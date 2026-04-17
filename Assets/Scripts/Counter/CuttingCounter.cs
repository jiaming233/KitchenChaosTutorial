using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public static event EventHandler OnCut;

    [SerializeField] private CuttingRecipeListSO cuttingRecipeListSO;

    private CuttingCounterVisual cuttingCounterVisual;

    [SerializeField] private ProgressBarUI progressBarUI;

    private int cuttingCount = 0;

    private void Start()
    {
        cuttingCounterVisual = GetComponentInChildren<CuttingCounterVisual>();
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                TransferKitchenObject(player, this);
                cuttingCount = 0;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
        }
    }

    public override void Operate(Player player)
    {
        base.Interact(player);

        if (HasKitchenObject())
        {
            if (cuttingRecipeListSO.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe))
            {
                Cut();

                progressBarUI.UpdateProgress((float)cuttingCount / cuttingRecipe.cuttingCountMax);

                if (cuttingCount == cuttingRecipe.cuttingCountMax)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                    cuttingCount = 0;
                    //KitchenObjectSO output = cuttingRecipeListSO.GetOutput(GetKitchenObject().GetKitchenObjectSO());
                }
            }
        }
    }

    private void Cut()
    {
        OnCut?.Invoke(this, EventArgs.Empty);
        cuttingCounterVisual.PlayCut();
        cuttingCount++;
    }

    public static void ClearStaticData()
    {
        OnCut = null;
    }
}

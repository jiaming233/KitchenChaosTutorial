using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FryingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int fryingTime;
}

[CreateAssetMenu]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipe> list;

    public bool TryGetFryingRecipe(KitchenObjectSO input, out FryingRecipe recipe)
    {
        foreach (FryingRecipe c in list)
        {
            if (c.input == input)
            {
                recipe = c;
                return true;
            }
        }
        recipe = null;
        return false;
    }
}

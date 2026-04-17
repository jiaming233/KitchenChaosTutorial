using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CuttingRecipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingCountMax;
}

[CreateAssetMenu]
public class CuttingRecipeListSO : ScriptableObject
{
    public List<CuttingRecipe> list;

    public KitchenObjectSO GetOutput(KitchenObjectSO input)
    {
        if(list == null || list.Count == 0)
            return null;
        foreach (CuttingRecipe c in list)
        {
            if (c.input == input)
                return c.output;
        }
        return null;
    }

    public bool TryGetCuttingRecipe(KitchenObjectSO input, out CuttingRecipe recipe)
    {
        foreach (CuttingRecipe c in list)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    public string recipeSOName;
    public List<KitchenObjectSO> kitchenObjectSOList;
}

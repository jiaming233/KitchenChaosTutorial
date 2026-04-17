using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeTemplate;

    private void Start()
    {
        recipeTemplate.gameObject.SetActive(false);
        OrderManager.Instance.OnOrderSpawned += OrderManager_OnOrderSpawned;
        OrderManager.Instance.OnOrderSpawned += OrderManager_OnOrderFinished;
    }

    private void OrderManager_OnOrderSpawned(object sender, EventArgs e)
    {
        UpdateUI();
    }
    private void OrderManager_OnOrderFinished(object sender, EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach(Transform child in recipeParent)
        {
            if(child != recipeTemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }

        var orderRecipeList= OrderManager.Instance.GetOrderRecipeSOList();
        foreach(var order in orderRecipeList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeTemplate, recipeParent);
            recipeUI.UpdateUI(order);
            recipeUI.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        OrderManager.Instance.OnOrderSpawned -= OrderManager_OnOrderSpawned;
        OrderManager.Instance.OnOrderSpawned -= OrderManager_OnOrderFinished;
    }
}

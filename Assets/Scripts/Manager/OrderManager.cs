using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public event EventHandler OnOrderSpawned;

    public event EventHandler OnOrderSuccess;
    public event EventHandler OnOrderFailed;

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float orderMaxCount = 5;
    [SerializeField] private float orderRate = 2;

    private List<RecipeSO> orderRecipeSOList = new List<RecipeSO>();

    private bool isStartOrder = false;
    private float orderTimer = 0;

    private int orderCount = 0;

    private int successOrderCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //isStartOrder = true;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            StartSpawnOrder();
        }
        else
        {
            isStartOrder = false;
        }
    }

    private void Update()
    {
        if (isStartOrder)
        {
            OrderUpdate();
        }
    }

    private void OrderUpdate()
    {
        orderTimer += Time.deltaTime;
        if (orderTimer >= orderRate)
        {
            orderTimer = 0;
            OrderANewRecipe();
        }
    }

    private void OrderANewRecipe()
    {
        if (orderCount >= orderMaxCount)
            return;
        orderCount++;
        int index = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
        orderRecipeSOList.Add(recipeListSO.recipeSOList[index]);
        OnOrderSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO correctRecipe = null;
        foreach (var recipe in orderRecipeSOList)
        {
            if (IsCorrect(recipe, plateKitchenObject))
            {
                correctRecipe = recipe;
                break;
            }
        }

        if (correctRecipe != null)
        {
            orderRecipeSOList.Remove(correctRecipe);
            successOrderCount++;
            OnOrderSuccess?.Invoke(this, EventArgs.Empty);
            Debug.Log("上菜正确");

        }
        else
        {
            OnOrderFailed?.Invoke(this, EventArgs.Empty);
            Debug.Log("上菜错误");
        }
    }

    private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();

        if (list1 == null || list2 == null)
            return false;
        if (list1.Count != list2.Count)
            return false;

        foreach (var item in list1)
        {
            if (!list2.Contains(item))
                return false;
        }
        return true;
    }

    public List<RecipeSO> GetOrderRecipeSOList()
    {
        return orderRecipeSOList;
    }

    private void StartSpawnOrder()
    {
        isStartOrder = true;
        successOrderCount = 0;
    }

    public int GetSuccessOrderCount()
    {
        return successOrderCount;
    }
}

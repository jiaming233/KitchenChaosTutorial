using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOs;

    [SerializeField] private PlateCompleteVisual plateCompleteVisual;

    [SerializeField] private KitchenObjectsGridUI kitchenObjectsGridUI;


    private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    public bool AddKitchenObject(KitchenObjectSO kitchenObject)
    {
        if (!validKitchenObjectSOs.Contains(kitchenObject))
        {
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObject))
        {
            return false;
        }
        plateCompleteVisual.ShowKitchenObject(kitchenObject);
        kitchenObjectsGridUI.ShowKitchenObjectUI(kitchenObject);
        kitchenObjectSOList.Add(kitchenObject);
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}

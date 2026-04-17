using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仓库类柜台
/// </summary>
public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ContainerCounterVisual containerCounterVisual;

    private void Start()
    {
        containerCounterVisual = GetComponentInChildren<ContainerCounterVisual>();
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        //if (GetKitchenObject() == null)
        //{
        //    SetKitchenObject(GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>());
        //}
        //else
        //{
        //    Debug.LogWarning("已有食材");
        //    TransferKitchenObject(this, player);
        //}

        if (player.HasKitchenObject())
            return;

        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);
        containerCounterVisual.PlayOpen();
    }
}
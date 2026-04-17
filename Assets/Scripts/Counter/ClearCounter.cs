using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                TransferKitchenObject(player, this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.AddKitchenObject(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        DestroyKitchenObject();
                    }
                }
                else
                {
                    if(GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject counterPlateKitchenObject))
                    {
                        if (counterPlateKitchenObject.AddKitchenObject(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
            }
            else
            {
                TransferKitchenObject(this, player);
            }
        }
    }
}
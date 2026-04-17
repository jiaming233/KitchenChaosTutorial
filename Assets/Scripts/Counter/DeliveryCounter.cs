using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    private const string IS_SHOW = "IsShow";

    [SerializeField] private Animator deliverySuccess;
    [SerializeField] private Animator deliveryFail;

    private void Start()
    {
        OrderManager.Instance.OnOrderSuccess += OrderManager_OnOrderSuccess;
        OrderManager.Instance.OnOrderFailed += OrderManager_OnOrderFailed;
    }

    private void OrderManager_OnOrderFailed(object sender, System.EventArgs e)
    {
        deliveryFail.gameObject.SetActive(true);
        deliveryFail.SetTrigger(IS_SHOW);
    }

    private void OrderManager_OnOrderSuccess(object sender, System.EventArgs e)
    {
        deliverySuccess.gameObject.SetActive(true);
        deliverySuccess.SetTrigger(IS_SHOW);
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                //判断上菜是否正确
                OrderManager.Instance.DeliveryRecipe(plateKitchenObject);

                player.DestroyKitchenObject();
            }        
        }
    }

    private void OnDestroy()
    {
        OrderManager.Instance.OnOrderSuccess -= OrderManager_OnOrderSuccess;
        OrderManager.Instance.OnOrderFailed -= OrderManager_OnOrderFailed;
    }
}

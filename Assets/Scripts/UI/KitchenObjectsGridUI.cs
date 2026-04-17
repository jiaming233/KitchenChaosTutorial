using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectsGridUI : MonoBehaviour
{
    [SerializeField] private KitchenObjectIconUI iconTemplate;

    public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObjectIconUI newIcon = GameObject.Instantiate<KitchenObjectIconUI>(iconTemplate, transform);
        newIcon.Show(kitchenObjectSO.sprite);
    }
}

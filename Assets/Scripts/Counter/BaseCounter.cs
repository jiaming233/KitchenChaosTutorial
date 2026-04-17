using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;

    public void SelectCounter()
    {
        selectedCounter.SetActive(true);
    }

    public void CancelSelect()
    {
        selectedCounter.SetActive(false);
    }

    public virtual void Interact(Player player)
    {
        //print(this.gameObject + "isInteracting");
    }

    public virtual void Operate(Player player)
    {
        //print(this.gameObject + "isInteracting");
    }
}

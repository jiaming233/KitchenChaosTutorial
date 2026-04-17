using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (player.HasKitchenObject())
        {
            player.DestroyKitchenObject();
            OnTrash?.Invoke(this, EventArgs.Empty);
        }
    }

    public static void ClearStaticData()
    {
        OnTrash = null;
    }
}

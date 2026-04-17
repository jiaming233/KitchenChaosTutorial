using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private float spawnRate = 3;
    [SerializeField] private int plateCountMax = 5;

    private float timer;

    private List<KitchenObject> platesList = new List<KitchenObject>();

    public override void Interact(Player player)
    {
        base.Interact(player);

        if (!player.HasKitchenObject())
        {
            if (platesList.Count > 0)
            {
                player.AddKitchenObject(platesList[platesList.Count - 1]);
                platesList.RemoveAt(platesList.Count - 1);
            }
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (platesList.Count < plateCountMax)
        {
            timer += Time.deltaTime;
        }
        if (timer > spawnRate)
        {
            SpawnPlate();
            timer = 0;
        }
    }

    private void SpawnPlate()
    {
        if (platesList.Count >= plateCountMax)
        {
            timer = 0;
            return;
        }

        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
        kitchenObject.transform.localPosition = Vector3.zero + 0.1f * platesList.Count * Vector3.up;
        platesList.Add(kitchenObject);
    }
}

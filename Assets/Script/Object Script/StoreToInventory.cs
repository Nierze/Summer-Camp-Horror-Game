using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StoreToInventory: MonoBehaviour
{
    public ItemsSO item;

    public InventoryManager updateItemsInInventory;

    void Start()
    {
        updateItemsInInventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    public void OnPickedUp()
    {
        InventoryManager.Instance.Add(item);
        updateItemsInInventory.ListItems();
        updateItemsInInventory.ListItems();
        UnityEngine.Debug.Log("Picked");
        Destroy(gameObject);
    }
}

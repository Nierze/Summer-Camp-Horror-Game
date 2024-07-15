using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class StoreToInventory: MonoBehaviour
{
    public ItemsSO item;
    public GameObject inventorySlot;
    //public InventoryManager updateItemsInInventory;

    void Start()
    {
        //updateItemsInInventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    /*public void OnPickedUp()
    {
        //InventoryManager.Instance.Add(item);
        updateItemsInInventory.Add(item);
        updateItemsInInventory.ListItems();
        updateItemsInInventory.ListItems();
        UnityEngine.Debug.Log("Picked");
        Destroy(gameObject);
    }*/

    public void StoreToInven()
    {
        FixedInventoryManager fixedInventoryManager = GameObject.Find("kfw - InventoryManager").GetComponent<FixedInventoryManager>();

        gameObject.transform.SetParent(fixedInventoryManager.inventoryObjects[fixedInventoryManager.emptyIndexInventory].transform);

        inventorySlot = GameObject.Find("kfw - Inventory").transform.Find("Viewport").gameObject.transform.Find("Content").transform.GetChild(fixedInventoryManager.emptyIndexInventory).gameObject;

        var itemName = inventorySlot.transform.Find("Item Name").GetComponent<TMP_Text>();
        var itemIcon = inventorySlot.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.objectIcon;

        //fixedInventoryManager.GetNearestEmpty();
        //fixedInventoryManager.CheckMaxInventory();

    }
}

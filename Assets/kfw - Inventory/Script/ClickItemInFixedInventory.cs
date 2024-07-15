using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.UI;

public class ClickItemInFixedInventory : MonoBehaviour
{
    public GameObject itemObject;
    public GameObject holdAreaObject;

    public ItemsSO itemInSlot;
    public StoreToInventory getItemSO;
    public Sprite baseItemIcon;
    
    void Start()
    {
        holdAreaObject = GameObject.Find("Hold Area");
        baseItemIcon = GameObject.Find("Empty Slot").GetComponent<Image>().sprite;
    }

    public void FetchItemInIventory()
    {
        if(gameObject.transform.childCount == 2)
        {
            UnityEngine.Debug.Log("This slot is empty");
        }
        else if(holdAreaObject.transform.childCount != 1)
        {
            itemObject = gameObject.transform.GetChild(2).gameObject;
            UnityEngine.Debug.Log(itemObject.name);
            itemObject.SetActive(true);

            Rigidbody itemRb = itemObject.GetComponent<Rigidbody>();
            itemRb.isKinematic = true;

            itemObject.transform.SetParent(holdAreaObject.transform);
            itemObject.transform.localPosition = Vector3.zero;

            UpdateSlot();
            UpdateNameAndIcon();
        }
    }

    public void UpdateSlot()
    {
        FixedInventoryManager fixedInventoryManager = GameObject.Find("kfw - InventoryManager").GetComponent<FixedInventoryManager>();
        fixedInventoryManager.GetNearestEmpty();
    }

    public void UpdateNameAndIcon()
    {
        if (gameObject.transform.childCount > 2)
        {
            getItemSO = gameObject.transform.GetChild(2).gameObject.GetComponent<StoreToInventory>();

            itemInSlot = getItemSO.item;
            var itemName = gameObject.transform.Find("Item Name").GetComponent<TMP_Text>();
            var itemIcon = gameObject.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

            if (itemName != null)
            {
                itemName.text = itemInSlot.itemName;
            }
            if (itemIcon != null)
            {
                itemIcon.sprite = itemInSlot.objectIcon;
            }
        }

        else
        {
            var itemName = gameObject.transform.Find("Item Name").GetComponent<TMP_Text>();
            var itemIcon = gameObject.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

            itemName.text = "Item Name";
            itemIcon.sprite = baseItemIcon;
        }
    }

}

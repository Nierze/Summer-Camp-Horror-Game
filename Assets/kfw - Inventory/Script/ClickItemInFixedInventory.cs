using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItemInFixedInventory : MonoBehaviour
{
    public GameObject itemObject;
    public GameObject holdAreaObject;

    void Start()
    {
        holdAreaObject = GameObject.Find("Hold Area");
    }

    public void FetchItemInIventory()
    {
        if(gameObject.transform.childCount != 3)
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
        }
    }

    public void UpdateSlot()
    {
        FixedInventoryManager fixedInventoryManager = GameObject.Find("kfw - InventoryManager").GetComponent<FixedInventoryManager>();
        fixedInventoryManager.GetNearestEmpty();

    }
}

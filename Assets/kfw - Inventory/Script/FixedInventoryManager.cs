using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedInventoryManager : MonoBehaviour
{
    public GameObject inventoryTab;

    public GameObject content;
    public GameObject[] inventoryObjects;
    public int emptyIndexInventory = 0;
    
    void Start()
    {
        inventoryTab = GameObject.Find("kfw - Inventory");
       

        content = inventoryTab.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject;
        inventoryObjects = new GameObject[content.transform.childCount];
        //UnityEngine.Debug.Log("inv elems = " + inventoryElements);
        
        for (int i = 0; i < content.transform.childCount; i++)
        {
            inventoryObjects[i] = content.transform.GetChild(i).gameObject;
            UnityEngine.Debug.Log(inventoryObjects[i]);
        }

        inventoryTab.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //inventoryTab.SetActive((inventoryTab.activeInHierarchy) ? false : true);
            if (inventoryTab.activeInHierarchy) InventoryTabManager(false);
            else InventoryTabManager(true);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            GetNearestEmpty();
        }
    }

    public void GetNearestEmpty()
    {
        if(emptyIndexInventory == content.transform.childCount - 1)
            UnityEngine.Debug.Log("Inventory Limit Reached");
        else
            for (int i = 0; i < content.transform.childCount; i++)
            {
                if(inventoryObjects[i].transform.childCount != 3)
                {
                    emptyIndexInventory = i;
                    UnityEngine.Debug.Log("empty inventory = " + inventoryObjects[i]);
                    break;
                }
            }
    }

    public void InventoryTabManager(bool status)
    {
        inventoryTab.SetActive(status);
        Cursor.visible = status;
    }
}

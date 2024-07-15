using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedInventoryManager : MonoBehaviour
{
    public GameObject inventoryTab;

    public GameObject content;
    public GameObject[] inventoryObjects;
    public int emptyIndexInventory = 0;

    public bool maxInventory = false;

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckMaxInventory();
        }

        CheckMaxInventory();
        //UnityEngine.Debug.Log("empty index inventory = "+ emptyIndexInventory);
        //UnityEngine.Debug.Log("Child Count = ");
        //UnityEngine.Debug.Log(content.transform.childCount - 1);
        //UnityEngine.Debug.Log("maxInventory = " + maxInventory);
    }

    public void GetNearestEmpty()
    {
        /*if (emptyIndexInventory == content.transform.childCount - 1)
        {
            UnityEngine.Debug.Log("Inventory Limit Reached");
            maxInventory = true;
        }
        else
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                if (inventoryObjects[i].transform.childCount != 3)
                {
                    emptyIndexInventory = i;
                    UnityEngine.Debug.Log("empty inventory = " + inventoryObjects[i]);
                    break;
                }
            }
            //maxInventory = false;
        }*/

        /*for (int i = 0; i < content.transform.childCount; i++)
        {
            UnityEngine.Debug.Log("inventory = " + inventoryObjects[i].name);
        }*/
        UnityEngine.Debug.Log("GetNearestEmpty() Section");

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (inventoryObjects[i].transform.childCount != 3)
            {
                emptyIndexInventory = i;
                UnityEngine.Debug.Log("empty inventory = " + inventoryObjects[i]);
                break;
            }
        }
        UnityEngine.Debug.Log("CheckMaxInventory() Section");
        CheckMaxInventory();
    }

    public void CheckMaxInventory()
    {
        //UnityEngine.Debug.Log("cnt.trn.chldcount = ");
        //UnityEngine.Debug.Log(content.transform.childCount - 1);
        //UnityEngine.Debug.Log(content.transform.childCount);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (inventoryObjects[i].transform.childCount == 3)
            {
                //UnityEngine.Debug.Log(inventoryObjects[i].name + "slot have object inside");
            }
            else
            {
                //UnityEngine.Debug.Log(inventoryObjects[i].name + "slot doesn't have object inside");
                maxInventory = false;
                return;
            }
        }
        maxInventory = true;
    }

    public void InventoryTabManager(bool status)
    {
        inventoryTab.SetActive(status);
        Cursor.visible = status;
    }
}

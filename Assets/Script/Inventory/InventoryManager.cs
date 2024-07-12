using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemsSO> itemsList = new List<ItemsSO>();

    [SerializeField] private Transform inventoryContents;
    [SerializeField] private GameObject itemPrefabTemplate;

    public GameObject inventory;
    public List<GameObject> physicalContents;

    public int inventoryItemsCount = 0;
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inventory = GameObject.Find("Inventory");
        inventory.SetActive(false);
        // List down the shits
        //ListItems();
    }

    void Start() { }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.activeInHierarchy)
            {
                inventory.SetActive(false);
                //UnityEngine.Debug.Log(inventory.activeInHierarchy);
            }
            else inventory.SetActive(true);
        }

        //UpdateList();
    }

    public void Add(ItemsSO item)
    {
        itemsList.Add(item);
        //physicalContents.Add();
    }

    public void DeleteRecent()
    {
        itemsList.RemoveAt(itemsList.Count - 1);
    }

    public void DeleteItem(ItemsSO item)
    {
        itemsList.Remove(item);
        ListItems();
    }

    public void ListItems()
    {
        foreach (ItemsSO item in itemsList)
        {
            UnityEngine.Debug.Log("item = " + item);
        }

        foreach (Transform child in inventoryContents)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemsSO item in itemsList)
        {
            UnityEngine.Debug.Log("item in list = " + item);

            GameObject itemObject = Instantiate(itemPrefabTemplate, inventoryContents);
            var itemName = itemPrefabTemplate.transform.Find("Item Name").GetComponent<TMP_Text>();
            var itemIcon = itemPrefabTemplate.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.objectIcon;

            GameObject itemPhysical = Instantiate(item.prefabObject);// .GetComponent<StoreToInventory>()) // prefabObject);
            //GameObject itemPhysical = Instantiate(itemObject.transform.GetChild(itemObject.transform.childCount - 1).gameObject);
            //StoreToInventory itemSO = itemPhysical.GetComponent<StoreToInventory>();
            //ItemsSO itemSOtoDelete = itemSO.item;
            //DeleteItem(itemSOtoDelete);

            itemPhysical.transform.SetParent(itemObject.transform);
            itemPhysical.transform.localPosition = new Vector3(0f, 0f, 0f);
            
            itemPhysical.SetActive(false);
        }
    }

    public void UpdateList()
    {
        for (int i = 0; i < gameObject.transform.childCount; i+=3)
        {
            itemsList.Add(gameObject.transform.GetChild(i).gameObject.GetComponent<ItemsSO>());
        }
    }
}
/*
            physicalContents.Add(item.prefabObject);
            GameObject physicalItem = Instantiate(item.prefabObject);
            Rigidbody itemRb = physicalItem.GetComponent<Rigidbody>();
            itemRb.isKinematic = true;
            itemRb.transform.position = new Vector3(0f, 0f, 0f);
            physicalItem.transform.SetParent(GameObject.Find("Hold Area").gameObject.transform);
 */

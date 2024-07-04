using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemsSO> itemsList = new List<ItemsSO>();


    public Transform inventoryContents;
    public GameObject itemPrefabTemplate;

    private void Awake() 
    {
        Instance = this;
    }


    public void Add(ItemsSO item) 
    {
        itemsList.Add(item);
    }

    public void ListItems()
    {

        foreach (Transform child in inventoryContents)
        {
            Destroy(child.gameObject);
        }
        
        foreach (ItemsSO item in itemsList)
        {
            GameObject itemObject = Instantiate(itemPrefabTemplate, inventoryContents);
            var itemName = itemPrefabTemplate.transform.Find("Item Name").GetComponent<TMP_Text>();
            var itemIcon = itemPrefabTemplate.transform.Find("Image").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.objectIcon;
        }
    }

    
}

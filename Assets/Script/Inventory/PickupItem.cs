using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line

public class PickupItem : MonoBehaviour
{
    private Button button;
    public ItemsSO item;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => InventoryManager.Instance.Add(item)); 
    }
}

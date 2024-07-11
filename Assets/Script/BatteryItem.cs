using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BatteryItem : MonoBehaviour
{
    public ItemsSO item;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnPickedUp();
        }
    }

    public void OnPickedUp()
    {
        InventoryManager.Instance.Add(item);
        UnityEngine.Debug.Log("Picked");
        Destroy(gameObject); // Destroy itself when picked up
    }
}

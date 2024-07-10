using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleFetchQuest : MonoBehaviour, ITaskInterface
{
    public static SampleFetchQuest Instance;

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void questRequirement() 
    {
        int hanabiFumoCount = 0;
        int requiredFumo = 4;
        foreach (ItemsSO item in InventoryManager.Instance.itemsList) 
        {
            if (item.itemName == "Hanabi Fumo") 
            {
                hanabiFumoCount++;
            }
        }
        if (hanabiFumoCount >= requiredFumo) 
        {
            Debug.Log("Quest Complete!");
        }
    }
}

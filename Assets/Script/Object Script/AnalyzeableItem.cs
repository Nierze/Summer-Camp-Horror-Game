using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assign to scannable objects to analyze them
//Contains attributes
public class AnalyzeableItem : MonoBehaviour
{
    [SerializeField]
    public string itemName;
    [SerializeField]
    public string type;

    void Start()
    {
        UnityEngine.Debug.Log(itemName + " " + type);
    }
}

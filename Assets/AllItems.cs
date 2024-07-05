using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LayerDefault()
    {
       foreach (Transform child in transform)
        {
            child.gameObject.layer = 0;

            foreach (Transform child2 in child)
            {
                child2.gameObject.layer = 0;
            }
            //Change layer inside the object
        }
    }
}

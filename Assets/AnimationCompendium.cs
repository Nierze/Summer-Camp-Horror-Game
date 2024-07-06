using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompendium : MonoBehaviour
{
    public Animator inventoryAnimator;
    public Animator objectiveAnimator;


    private bool inventoryopen;
    private bool objectivesopen;

    // Start is called before the first frame update
    void Start()
    {
        inventoryopen = false;
        objectivesopen = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryButton()
    {
        if (!inventoryopen) {
            OpenInventory();
        }
        else if (inventoryopen)
        {
            CloseInventory();
        }
    }

    public void ObjectivesButton()
    {
        if (!objectivesopen)
        {
            OpenObjectives();
        }
        else if (objectivesopen)
        {
            CloseObjectives();
        }
    }

    public void OpenInventory()
    {
        inventoryAnimator.SetTrigger("Open Inventory");
        inventoryAnimator.ResetTrigger("Close Inventory");
        inventoryopen = true;
    }

    public void CloseInventory()
    {
        inventoryAnimator.SetTrigger("Close Inventory");
        inventoryAnimator.ResetTrigger("Open Inventory");
        inventoryopen = false;

    }

    public void OpenObjectives()
    {
        objectiveAnimator.SetTrigger("Open Objectives");
        objectiveAnimator.ResetTrigger("Close Objectives");
        objectivesopen = true;
    }

    public void CloseObjectives()
    {
        objectiveAnimator.SetTrigger("Close Objectives");
        objectiveAnimator.ResetTrigger("Open Objectives");
        objectivesopen = false;

    }
}

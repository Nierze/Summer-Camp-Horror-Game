using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTask : MonoBehaviour
{
    // Start is called before the first frame update

    public TaskScriptableObject task;

    public void addTask()
    {
        TaskHandler.Instance.add(task);
    }
}

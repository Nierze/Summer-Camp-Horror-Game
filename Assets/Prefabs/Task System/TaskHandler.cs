using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskHandler : MonoBehaviour
{
    // Singleton pattern
    public static TaskHandler Instance;

    // List of tasks
    public List<TaskScriptableObject> taskList = new List<TaskScriptableObject>();

    [SerializeField] private Transform taskContents;
    [SerializeField] private GameObject taskPrefabTemplate;

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

        // List down the shits
    } 


    public void add(TaskScriptableObject task)
    {
        taskList.Add(task);
    }

    public void ListItems()
    {
        foreach (Transform child in taskContents)
        {
            Destroy(child.gameObject);
        }
        
        foreach (TaskScriptableObject task in taskList)
        {
            GameObject taskObject = Instantiate(taskPrefabTemplate, taskContents);
            var taskName = taskPrefabTemplate.transform.Find("Task Title").GetComponent<TMP_Text>();
            var taskDescription = taskPrefabTemplate.transform.Find("Task Description").GetComponent<TMP_Text>();
            // var taskCheckbox = itemPrefabTemplate.transform.Find("Checkbox").GetComponent<Image>();

            taskName.text = task.taskName;
            taskDescription.text = task.taskDescription;
        }
    }

    


}

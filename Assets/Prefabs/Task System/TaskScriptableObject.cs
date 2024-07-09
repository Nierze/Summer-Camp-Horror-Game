using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task #?", menuName = "Create Task Scriptable Object")]
public class TaskScriptableObject : ScriptableObject
{
    [SerializeReference] 
    public string taskName;
    public string taskDescription;
    public TaskType taskType = TaskType.None;
    public bool isComplete;

    [Header("ID must be a 4 digit number (e.g. 0001)")]
    public string taskID; 
}

public enum TaskType
{
    None,
    KillQuest,
    SearchQuest,
    FetchQuest,
}



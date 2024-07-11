using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AddTask : MonoBehaviour
{
    // Start is called before the first frame update

    /* remove this comment, and comment "jm changes" to revert from pre-updated script
    public TaskScriptableObject task;

    
    public void addTask()
    {
        TaskHandler.Instance.add(task);
    }
    */

    ///////////////////////
    //jm changes;
    
    public List<TaskScriptableObject> tasks;
    int taskCount = 0;
    int maxTask = 0;
    int taskRandomizer;

    //public TaskScriptableObject task;
    public void addTask()
    {
        //difficulty section forced setting of difficulty
        //section to be commented/removed when scene is loaded from "Main Menu" Scene
        DifficultySelector.setDifficulty = "easy";
        UnityEngine.Debug.Log(DifficultySelector.setDifficulty);
        //end of difficulty section

        if (DifficultySelector.setDifficulty == "easy") maxTask = 3;
        else if (DifficultySelector.setDifficulty == "normal") maxTask = 4;
        else if (DifficultySelector.setDifficulty == "hard") maxTask = 5;

        if (taskCount < maxTask)
        {
            StartCoroutine(generateTask());
        }
    }

    private IEnumerator generateTask()
    {
        while (taskCount < maxTask)
        {
            taskRandomizer = UnityEngine.Random.Range(0, 3);
            TaskHandler.Instance.add(tasks[taskRandomizer]);
            taskCount++;
            yield return null;
        }
        TaskHandler.Instance.ListItems();
        yield return null;
    }
}

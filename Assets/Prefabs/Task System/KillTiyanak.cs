using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTiyanak : ITaskInterface
{
    public List<string> enemiesKilled = new List<string>();
    public int killCount = 0;
    public int killGoal = 1;
    public string target = "Tiyanak";

    public void questRequirement()
    {
        foreach (string enemy in enemiesKilled)
        {
            if (enemy == target)
            {
                killCount++;
            }
        }

        if (killCount >= killGoal)
        {
            Debug.Log("Quest Completed!");
        }
        else
        {
            Debug.Log($"Kill {killCount}/{killGoal} {target}.");
        }
    }
}

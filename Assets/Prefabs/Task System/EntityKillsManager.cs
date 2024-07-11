using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityKillsManager : MonoBehaviour
{
    public List<KillableEntity> killableEntity = new List<KillableEntity>();
    public List<KillableEntity> killedEntities = new List<KillableEntity>();

    public void Awake() 
    {
        KillableEntity[] killableEntities = FindObjectsOfType<KillableEntity>();
        killableEntity.AddRange(killableEntities);
    }



}

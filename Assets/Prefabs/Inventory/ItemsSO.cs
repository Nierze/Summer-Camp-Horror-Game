using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item_Object_", menuName = "New ItemSSO")]
public class ItemsSO : ScriptableObject
{
    public string itemName;
    public string description;

    public Sprite objectSprite;
    public Sprite objectIcon;

    public float healthValue;
    public float sanityValue;
    public float damageValue;

    public float damageReductionMultiplier;

    public GameObject prefabObject;

    public MonsterEffectiveness monsterEffectiveness;

    public ObjectType objectType;
}

public enum MonsterEffectiveness
{
    None,
    Tiyanak,
    Pugot,
    Utility
}

public enum ObjectType
{
    Consumable,
    Throwable,
    Utility
}

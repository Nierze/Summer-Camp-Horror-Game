using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item_Object_", menuName = "Scriptable Objects")]
public class ItemsSO : ScriptableObject
{
    public string itemName;
    public string description;

    public Image objectSprite;
    public Image objectIcon;

    public float healthValue;
    public float sanityValue;
    public float damageValue;

    public float damageReductionMultiplier;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Flashlight,
    Key
}
public abstract class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public ItemType itemType;
    public abstract void Use();
}

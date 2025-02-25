using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flashlight", menuName = "Inventory System/Items/Flashlight")]
public class FlashlightScriptableObject : ItemScriptableObject
{
    public void Awake()
    {
        itemType = ItemType.Flashlight;
    }
    
    public override void Use()
    {
        Debug.Log("Using Flashlight");
    }
}

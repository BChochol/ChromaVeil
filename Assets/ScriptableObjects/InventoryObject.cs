using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<ItemScriptableObject> items = new List<ItemScriptableObject>();

    public void AddItem(ItemScriptableObject item)
    {
            items.Add(item);
    }

    public void RemoveItem(ItemScriptableObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
    }
}

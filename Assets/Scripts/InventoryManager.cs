using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    
    public void AddItemToInventory(ItemScriptableObject item)
    {
        Debug.Log("added " + item.itemName + " to inventory");
        inventory.AddItem(item);
    }
    
    public void RemoveItemFromInventory(ItemScriptableObject item)
    {
        inventory.RemoveItem(item);
    }
}

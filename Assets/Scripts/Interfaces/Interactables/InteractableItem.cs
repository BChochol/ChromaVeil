using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public GameObject interactableObject { get; set; }
    public ItemScriptableObject item;
    [SerializeField] InventoryManager _inventoryManager;
    
    public void Interact()
    {
        Debug.Log("Interacted with ");
        _inventoryManager.AddItemToInventory(item);
    
    }
    private void Awake()
    {
        AllInteractablesController.Register(this);
    }
    
    private void OnDestroy()
    {
        AllInteractablesController.Unregister(this);
    }

}

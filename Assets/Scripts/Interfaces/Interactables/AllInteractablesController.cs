using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllInteractablesController : MonoBehaviour
{
    
    private static List<IInteractable> _allInteractables = new();
    
    public static void Register(IInteractable interactable)
    {
        _allInteractables.Add(interactable);
    }
    
    public static void Unregister(IInteractable interactable)
    {
        _allInteractables.Remove(interactable);
    }
    
}

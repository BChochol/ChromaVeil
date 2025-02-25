using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllReactablesController : MonoBehaviour
{
    
    private static List<IReactable> _toReact = new();
    
    public static void Register(IReactable reactable)
    {
        _toReact.Add(reactable);
    }
    
    public static void Unregister(IReactable reactable)
    {
        _toReact.Remove(reactable);
    }

    public static void ReactToColorChange()
    {
        foreach (var reactable in _toReact)
        {
           reactable.ReactToColorChange();
        }
    }
    
    
}

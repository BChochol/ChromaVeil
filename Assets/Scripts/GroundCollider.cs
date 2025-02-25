using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    
    public static List<Collision> _groundedObjects = new();
    
    
    private void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Wall")
        _groundedObjects.Add(collidedObject);
    }
    
    private void OnCollisionExit(Collision collidedObject)
    {
        if (_groundedObjects.Contains(collidedObject))
        {
            _groundedObjects.Remove(collidedObject);
        }
    }
}

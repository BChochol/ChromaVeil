using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] public List<GameObject> _interactableObjects = new();
    [SerializeField] private GameObject _textField;

    private void OnCollisionEnter(Collision collidedObject)
    {
        if (collidedObject.gameObject.tag == "Interactable")
        {
            Debug.Log("Added " + collidedObject.gameObject.name + " to interactable objects list.");
            _interactableObjects.Add(collidedObject.gameObject);
            _textField.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collidedObject)
    {
        Debug.Log("Removed " + collidedObject.gameObject.name + " from interactable objects list.");
        if (_interactableObjects.Contains(collidedObject.gameObject))
        {
            _interactableObjects.Remove(collidedObject.gameObject);
            if (_interactableObjects.Count == 0)
            {
                _textField.SetActive(false);
            }
        }
    }

    private void Update()
    {
        CheckInteractionTrigger();
    }
    
    private void CheckInteractionTrigger()
    {
        if (Inputs.IsInteracting())
        {
            if (_interactableObjects.Count > 0)
            {
                IInteractable interactable = _interactableObjects[0].gameObject.GetComponent<IInteractable>();
                interactable.Interact();
            }
        }
    }
}

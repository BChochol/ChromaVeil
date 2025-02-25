using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    [SerializeField] GameObject interactableObject { get; set; }
    public void Interact();

}

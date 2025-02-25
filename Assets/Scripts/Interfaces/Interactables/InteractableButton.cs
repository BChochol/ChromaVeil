using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _interactableButton;
    [SerializeField] private ColorEnum.ColorType _colorType;
    [SerializeField] private ColorController _colorController;
    [SerializeField] private bool _isOn = false;
    
    public GameObject interactableObject
    {
        get => _interactableButton;
        set => _interactableButton = value;
    }
    
    public void Interact()
    {
        //Debug.Log("Interacted with " + interactableObject.name);
        //switch (_isOn)
        List<ColorEnum.ColorType> colors = _colorController.GetColors();
        switch(colors.Contains(_colorType))
        {
            case false:
                _colorController.AddColor(_colorType);
                _isOn = true;
                AudioManager.Instance.PlaySFXClickOn();
                break;
            case true:
                _colorController.SubtractColor(_colorType);
                _isOn = false;
                AudioManager.Instance.PlaySFXClickOff();
                break;
        }
        
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

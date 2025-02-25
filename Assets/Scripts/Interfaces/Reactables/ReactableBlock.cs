using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ReactableBlock : MonoBehaviour, IReactable
{
    [SerializeField] private GameObject _reactableBlock;
    [SerializeField] private List<ColorEnum.ColorType> _reactingColorsSetter;


    public GameObject _reactableObject
    {
        get => _reactableBlock;
        set => _reactableBlock = value;
    }

    public List<ColorEnum.ColorType> _reactingColors
    {
        get => _reactingColorsSetter;
        set => _reactingColorsSetter = value;
    }

    public void Start()
    {
        // switch (_reactingColorsSetter)
        // {
        //     case ColorEnum.ColorType.Red:
        //         _reactableBlock.GetComponent<Renderer>().material.color = Color.red;
        //         break;
        //     case ColorEnum.ColorType.Blue:
        //         _reactableBlock.GetComponent<Renderer>().material.color = Color.blue;
        //         break;
        //     case ColorEnum.ColorType.Green:
        //         _reactableBlock.GetComponent<Renderer>().material.color = Color.green;
        //         break;
        // }
    }
    
    public void Awake()
    {
        AllReactablesController.Register(this);
    }
    
    public void OnDestroy()
    {
        AllReactablesController.Unregister(this);
    }
    
    public void ReactToColorChange()
    {
        if(_reactingColors.All(color => ColorController._litColors.Contains(color) && 
                                        ColorController._litColors.All(color => _reactingColors.Contains(color))))
            _reactableBlock.SetActive(false);
        else
            _reactableBlock.SetActive(true);
    }

    
}

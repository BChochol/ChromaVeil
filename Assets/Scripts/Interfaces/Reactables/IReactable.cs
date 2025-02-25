using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactable
{
    [SerializeField] GameObject _reactableObject { get; set; }
    [SerializeField] List<ColorEnum.ColorType> _reactingColors { get; set; }
    public void ReactToColorChange();
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public static List<ColorEnum.ColorType> _litColors = new();
    private Light _light;
    [SerializeField] public GameObject _colorLight;
    [SerializeField] public GameObject _mainLight;
    
    public Image greenElement;
    public Image redElement;
    public Image blueElement;
    public Image cyanElement;
    public Image yellowElement;
    public Image magentaElement;
    public Image whiteElement;


    public void Awake()
    {
        _light = _colorLight.gameObject.GetComponent<Light>();
    }
    public void AddColor(ColorEnum.ColorType colorToAdd)
    {
        UpdateLight(colorToAdd, 1);
        _litColors.Add(colorToAdd);
        AllReactablesController.ReactToColorChange();
        UpdateMainLight();
        UpdateColorsUI();
    }
    
    public void SubtractColor(ColorEnum.ColorType colorToSubtract)
    {
        _litColors.Remove(colorToSubtract);
        UpdateLight(colorToSubtract, -1);
        AllReactablesController.ReactToColorChange();
        UpdateMainLight();
        UpdateColorsUI();
    }
    
    public List<ColorEnum.ColorType> GetColors()
    {
        return _litColors;
    }

    public void UpdateLight(ColorEnum.ColorType colorToUpdate, int isAdded)
    {
        if (_litColors.Contains(colorToUpdate)) return;
        
        
        switch (colorToUpdate)
        {
            case ColorEnum.ColorType.Red:
                _light.color = new Color(_light.color.r + 1.0f * Mathf.Sign(isAdded), _light.color.g, _light.color.b, 0.5f);
                break;
            case ColorEnum.ColorType.Blue:
                _light.color = new Color(_light.color.r, _light.color.g, _light.color.b + 1.0f * Mathf.Sign(isAdded), 0.5f);
                break;
            case ColorEnum.ColorType.Green:
                _light.color = new Color(_light.color.r, _light.color.g + 1.0f * Mathf.Sign(isAdded), _light.color.b, 0.5f);
                break;
        }
    }
    
    public void OnDestroy()
    {
        _litColors.Clear();
    }
    
    public void UpdateMainLight()
    {
        if (_litColors.Count == 0)
        {
            //Debug.Log("Main light is on");
            _mainLight.gameObject.GetComponent<Light>().intensity = 1f;
        }
        else
        {
            //Debug.Log("Main light is off");
            _mainLight.gameObject.GetComponent<Light>().intensity = 0.3f;
        }
    }
    
    private void UpdateColorsUI()
    {
        bool isGreenLit = _litColors.Contains(ColorEnum.ColorType.Green);
        bool isRedLit = _litColors.Contains(ColorEnum.ColorType.Red);
        bool isBlueLit = _litColors.Contains(ColorEnum.ColorType.Blue);

        SetElementAlpha(greenElement, isGreenLit ? 0.95f : 0.1f);
        SetElementAlpha(redElement, isRedLit ? 0.95f : 0.1f);
        SetElementAlpha(blueElement, isBlueLit ? 0.95f : 0.1f);

        SetElementAlpha(cyanElement, (isGreenLit && isBlueLit) ? 0.95f : 0.1f);
        SetElementAlpha(yellowElement, (isGreenLit && isRedLit) ? 0.95f : 0.1f);
        SetElementAlpha(magentaElement, (isRedLit && isBlueLit) ? 0.95f : 0.1f);

        SetElementAlpha(whiteElement, (isGreenLit && isRedLit && isBlueLit) ? 0.95f : 0.1f);
    }
    
    private void SetElementAlpha(Image element, float alpha)
    {
        if (element != null)
        {
            Color color = element.color;
            color.a = alpha;
            element.color = color;
        }
    }


}

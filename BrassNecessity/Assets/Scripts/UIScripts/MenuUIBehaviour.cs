using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class MenuUIBehaviour : MonoBehaviour, IMenuUIBehaviour
{
    protected VisualElement rootVisualElement;
    [SerializeField]
    protected MenuButtonData[] allButtonData;
    protected Dictionary<string, GenericButton> buttonMap;

    protected void toggleButtonSelectClass(string elementName)
    {
        buttonMap[elementName].ToggleSelect();
    }

    protected abstract void setupMenu();

    public abstract void NavigateToNextElement(Vector2 direction);
    public abstract void ExecuteCurrentButton();
}

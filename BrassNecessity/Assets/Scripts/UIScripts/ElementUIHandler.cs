using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementUIHandler : MonoBehaviour
{
    [SerializeField]
    private ElementApplyState playerElementSource;
    [SerializeField]
    private ElementComponent playerLaserElement;
    [SerializeField]
    private WeaponBehaviour playerWeapon;
    [SerializeField]
    private ElementBrokenState brokenElementState;
    [SerializeField]
    private Color elementOverloadColor = Color.red;
    [SerializeField]
    private Color brokenElementColor = Color.black;
    [SerializeField]
    private Color elementsDisabledColor = Color.gray;
    private VisualElement rootVisualElement;
    private VisualElement elementQueueElement;
    private VisualElement currentElement;
    private ElementData dataReference;
    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        elementQueueElement = rootVisualElement.Q<VisualElement>("ElementQueue");
        dataReference = FindObjectOfType<ElementData>();
        if (playerElementSource == null)
        {
            playerElementSource = FindObjectOfType<ElementApplyState>();
        }
        if (playerWeapon == null)
        {
            playerWeapon = FindObjectOfType<WeaponBehaviour>();
        }
        if (playerLaserElement == null)
        {
            playerLaserElement = playerWeapon.gameObject.GetComponent<ElementComponent>();
        }
        if (brokenElementState == null)
        {
            brokenElementState = FindObjectOfType<ElementBrokenState>();
        }
        currentElement = rootVisualElement.Q<VisualElement>("CurrentElement");

        if (playerLaserElement.ElementInfo != null)
        {
            setCurrentElement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        reloadElementQueue();
        setCurrentElement();
    }

    private void clearCurrentElements()
    {
        List<VisualElement> queueChildElements = new List<VisualElement>(elementQueueElement.Children());
        for (int i = 0; i < queueChildElements.Count; i++)
        {
            elementQueueElement.Remove(queueChildElements[i]);
        }
    }

    private void reloadElementQueue()
    {
        clearCurrentElements();
        Queue<ElementComponent> elementsToApply = playerElementSource.GetElementsCopy();
        while (elementsToApply.Count > 0)
        {
            ElementComponent component = elementsToApply.Dequeue();
            VisualElement newQueuedElement = new VisualElement();
            newQueuedElement.ToggleInClassList("elementImage");
            newQueuedElement.style.backgroundColor = dataReference.GetLight(component.ElementInfo);
            elementQueueElement.Add(newQueuedElement);
        }
    }

    private void setCurrentElement()
    {
        float overloadPercent = playerWeapon.ElementPercentRemaining();
        if (Mathf.Approximately(overloadPercent, 0f))
        {
            float recoveryPercent = brokenElementState.RecoveryPercentRemaining();
            if (recoveryPercent < 0.5f)
            {
                overlayElementColor(recoveryPercent, brokenElementColor, Color.clear);
            }
            else
            {
                overlayElementColor(overloadPercent, brokenElementColor);
            }
        }
        else
        {
            overlayElementColor(overloadPercent, elementOverloadColor);
        }
    }

    private void overlayElementColor(float percentLerp, Color colorToOverlay)
    {
        Color baseColor = dataReference.GetLight(playerLaserElement.ElementInfo);
        Color backgroundColor = Color.Lerp(colorToOverlay, baseColor, percentLerp);
        Color baseBorderColor = Color.white;
        Color borderColor = Color.Lerp(colorToOverlay, baseBorderColor, percentLerp);
        currentElement.style.backgroundColor = backgroundColor;
        currentElement.style.borderTopColor = borderColor;
        currentElement.style.borderLeftColor = borderColor;
        currentElement.style.borderRightColor = borderColor;
        currentElement.style.borderBottomColor = borderColor;
    }

    private void overlayElementColor(float percentLerp, Color overrideBaseColor, Color colorToOverlay)
    {
        Color overlayColor = Color.Lerp(colorToOverlay, overrideBaseColor, percentLerp);
        currentElement.style.backgroundColor = overlayColor;
        currentElement.style.borderTopColor = overlayColor;
        currentElement.style.borderLeftColor = overlayColor;
        currentElement.style.borderRightColor = overlayColor;
        currentElement.style.borderBottomColor = overlayColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ElementComponent : MonoBehaviour
{
    public ElementPair ElementInfo { get; private set; }
    [SerializeField]
    private Element.Type primaryType;
    [SerializeField]
    private Element.Type secondaryType;

    private void Awake()
    {
        if (!Application.IsPlaying(gameObject))
        {
            ElementInfo = new ElementPair(primaryType);
        }
        else if (ElementInfo == null)
        {
            ElementInfo = new ElementPair(primaryType);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ElementInfo == null)
        {
            ElementInfo = new ElementPair(primaryType, secondaryType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            ElementInfo = new ElementPair(primaryType, secondaryType);
        }
    }

    public void SwitchType(Element.Type newType)
    {
        primaryType = newType;
        ElementInfo = new ElementPair(newType);
        secondaryType = ElementInfo.Secondary;
    }

    public void SwitchType(Element.Type newPrimaryType, Element.Type newSecondaryType)
    {
        primaryType = newPrimaryType;
        secondaryType = newSecondaryType;
        ElementInfo = new ElementPair(newPrimaryType, newSecondaryType);
    }

    public void SwitchTypeRandom()
    {
        ElementInfo = new ElementPair();
        primaryType = ElementInfo.Primary;
        secondaryType = ElementInfo.Secondary;
    }

    public void UpdateElement()
    {
        SwitchType(primaryType);
    }
}

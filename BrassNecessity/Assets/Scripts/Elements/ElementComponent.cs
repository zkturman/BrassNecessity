using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementComponent : MonoBehaviour
{
    public ElementPair ElementInfo { get; private set; }
    [SerializeField]
    private ElementType primaryType;
    [SerializeField]
    private ElementType secondaryType;

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
        
    }

    public void SwitchType(ElementType newType)
    {
        primaryType = newType;
        ElementInfo = new ElementPair(newType);
    }

    public void SwitchType(ElementType newPrimaryType, ElementType newSecondaryType)
    {
        primaryType = newPrimaryType;
        secondaryType = newSecondaryType;
        ElementInfo = new ElementPair(newPrimaryType, newSecondaryType);
    }
}

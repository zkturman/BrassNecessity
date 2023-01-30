using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour
{
    private ElementPair elementInfo;
    [SerializeField]
    private ElementType primaryType;
    [SerializeField]
    private ElementType secondaryType;

    private void Awake()
    {
        elementInfo = new ElementPair(primaryType, secondaryType);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchType(ElementType newType)
    {
        elementInfo = new ElementPair(newType);
    }

    public void SwitchType(ElementType newPrimaryType, ElementType newSecondaryType)
    {
        elementInfo = new ElementPair(newPrimaryType, newSecondaryType);
    }
}

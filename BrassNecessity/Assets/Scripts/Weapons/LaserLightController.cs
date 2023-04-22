using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightController : MonoBehaviour
{
    [SerializeField]
    private ElementComponent elementComponent;
    private Element.Type lastType;

    [SerializeField]
    private ElementData data;

    [SerializeField]
    private VolumetricLines.VolumetricLineBehavior laser;

    [SerializeField]
    private GameObject laserSplash;

    [SerializeField]
    private ImpactBehaviour laserBaseEffect;

    private void Awake()
    {
        if (data == null)
        {
            data = FindObjectOfType<ElementData>();
        }
    }

    private void Start()
    {
        setColors(elementComponent.ElementInfo);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastType != elementComponent.ElementInfo.Primary)
        {
            setColors(elementComponent.ElementInfo);
        }        
    }

    private void setColors(ElementPair element)
    {
        Color elementColor = data.GetLight(element);
        laser.LineColor = elementColor;
        laserSplash.GetComponent<MeshRenderer>().material = data.GetBatteryMaterial(element);
        laserBaseEffect.SetColor(elementColor);
        lastType = element.Primary;
    }
}

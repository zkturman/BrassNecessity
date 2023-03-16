using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightController : MonoBehaviour
{
    [SerializeField]
    private ElementComponent elementInfo;
    private Element.Type lastType;

    [SerializeField]
    private ElementData data;

    [SerializeField]
    private VolumetricLines.VolumetricLineBehavior laser;

    [SerializeField]
    private GameObject laserSplash;

    [SerializeField]
    private ParticleSystem laserBaseEffect;

    // Update is called once per frame
    void Update()
    {
        if (lastType != elementInfo.ElementInfo.Primary)
        {
            laser.LineColor = data.GetLight(elementInfo.ElementInfo);
            laserSplash.GetComponent<MeshRenderer>().material = data.GetBatteryMaterial(elementInfo.ElementInfo);
            var baseEffectMain = laserBaseEffect.main;
            baseEffectMain.startColor = data.GetLight(elementInfo.ElementInfo);
            lastType = elementInfo.ElementInfo.Primary;
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBehaviour : MonoBehaviour
{
    [SerializeField]
    private float weakScale = 100f;
    [SerializeField]
    private float normalScale = 200f;
    [SerializeField]
    private float strongScale = 300f;
    [SerializeField]
    private float weakPulse = .025f;
    [SerializeField]
    private float normalPulse = 0.38f;
    [SerializeField]
    private float strongPulse = 0.5f;

    [SerializeField]
    ParticleSystem spreadEffect;

    [SerializeField]
    ParticleSystem initialImpact;

    public void SetImpactEffects(float attackMultiplier)
    {
        setScale(attackMultiplier);
        setPulseRate(attackMultiplier);
    }

    private void setScale(float attackMultiplier)
    {
        Vector3 newScale;
        if (ElementMultiplierGrid.IsStrongMultiplier(attackMultiplier))
        {
            newScale = new Vector3(strongScale, strongScale, strongScale);
        }
        else if (ElementMultiplierGrid.IsWeakMultiplier(attackMultiplier))
        {
            newScale = new Vector3(weakScale, weakScale, weakScale);
        }
        else
        {
            newScale = new Vector3(normalScale, normalScale, normalScale);
        }
        transform.localScale = newScale;
    }

    private void setPulseRate(float attackMultiplier)
    {
        var initialImpactMain = initialImpact.main;
        if (ElementMultiplierGrid.IsStrongMultiplier(attackMultiplier))
        {
            initialImpactMain.startSize = strongPulse;
        }
        else if (ElementMultiplierGrid.IsWeakMultiplier(attackMultiplier))
        {
            initialImpactMain.startSize = weakPulse;
        }
        else
        {
            initialImpactMain.startSize = normalPulse;
        }
    }

    public void SetColor(Color colorToSet)
    {
        setParticleSystemStartColor(initialImpact, colorToSet);
        setParticleSystemStartColor(spreadEffect, colorToSet);
    }

    private void setParticleSystemStartColor(ParticleSystem system, Color colorToSet)
    {
        var systemMain = system.main;
        systemMain.startColor = colorToSet;
    }

    public void ResetEffects()
    {
        setScale(normalScale);
        setPulseRate(normalPulse);
    }
}

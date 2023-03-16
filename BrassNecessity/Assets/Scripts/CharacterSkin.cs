using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterSkin
{
    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    private GameObject[] accessories;

    public void ActivateSkin()
    {
        mesh.SetActive(true);
        setActiveAccessories(true);
    }

    public void DeactivateSkin()
    {
        mesh.SetActive(false);
        setActiveAccessories(false);
    }

    private void setActiveAccessories(bool isActive)
    {
        for (int i = 0; i < accessories.Length; i++)
        {
            accessories[i].SetActive(isActive);
        }
    }
}

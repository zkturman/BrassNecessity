using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private int characterId = 0;
    private int lastCharacterId = 0;

    [SerializeField]
    private SkinSelector skinSelector;
    private void Awake()
    {
        skinSelector = GetComponent<SkinSelector>();
        skinSelector.SelectSkin(characterId);
    }

    // Update is called once per frame
    void Update()
    {
        if (skinSelector == null)
        {
            skinSelector = GetComponent<SkinSelector>();
        }
        if (lastCharacterId != characterId)
        {
            skinSelector.SelectSkin(characterId);
            lastCharacterId = characterId;
        }
    }
}

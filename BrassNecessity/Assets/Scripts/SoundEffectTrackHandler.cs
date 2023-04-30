using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTrackHandler : MonoBehaviour
{
    [SerializeField]
    private SoundEffectKeyValue[] clipListing;
    private Dictionary<SoundEffectKey, SoundEffectKeyValue> listingMap;
    [SerializeField]
    private AudioListener playerCameraListener;

    private void Awake()
    {
        if (playerCameraListener == null)
        {
            playerCameraListener = Camera.main.GetComponent<AudioListener>();
        }
        listingMap = new Dictionary<SoundEffectKey, SoundEffectKeyValue>();
        for (int i = 0; i < clipListing.Length; i++)
        {
            listingMap.Add(clipListing[i].Key, clipListing[i]);
        }
    }

    public void PlayOnce(SoundEffectKey listingKey)
    {
        AudioClip clipToPlay = listingMap[listingKey].Value;
        AudioSource.PlayClipAtPoint(clipToPlay, playerCameraListener.transform.position);
    }

    public AudioClip GetClipForLooping(SoundEffectKey listingKey)
    {
        return listingMap[listingKey].Value;
    }
}

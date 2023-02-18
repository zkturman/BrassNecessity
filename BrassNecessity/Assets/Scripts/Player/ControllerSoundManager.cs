using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ControllerSoundManager
{
    [SerializeField]
    private AudioClip landingAudioClip;

    [SerializeField]
    private AudioClip[] footstepAudioClips;

    [SerializeField]
    [Range(0, 1)]
    private float footstepAudioVolume = 0.5f;

    public void PlayFootStepSound(Vector3 soundSourceLocation)
    {
        if (footstepAudioClips.Length > 0)
        {
            var index = UnityEngine.Random.Range(0, footstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(footstepAudioClips[index], soundSourceLocation, footstepAudioVolume);
        }
    }

    public void PlayLandSound(Vector3 soundSourceLocation)
    {
        AudioSource.PlayClipAtPoint(landingAudioClip, soundSourceLocation, footstepAudioVolume);

    }
}

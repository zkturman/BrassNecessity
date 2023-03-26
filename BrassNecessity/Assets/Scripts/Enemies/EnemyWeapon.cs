using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class EnemyWeapon : MonoBehaviour
{
    BoxCollider hitDetectCollider;
    AudioSource hitSoundAudioSource;

    private void Awake()
    {
        hitSoundAudioSource = GetComponent<AudioSource>();
        hitDetectCollider = GetComponent<BoxCollider>();
        hitDetectCollider.isTrigger = true;
    }


    public void ActivateWeapon()
    {
        hitDetectCollider.enabled = true;
        //Debug.Log("Activate weapon");
    }


    public void DeactivateWeapon()
    {
        hitDetectCollider.enabled = false;
        //Debug.Log("Deactivate weapon");
    }


    //private void OnTriggerEnter(Collider other)
    private void OnCollisionEnter(Collision collision)
    {
        // If player gets hit
        //if (other.CompareTag("Player"))
        if (collision.gameObject.CompareTag("Player"))
        {
            hitSoundAudioSource.Play();
            
            // *** Insert code here to tell the player they have been hit ***
            
            DeactivateWeapon();

        }
    }

}

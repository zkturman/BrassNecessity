using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
        
    public BoxCollider hitBox;

    private void Awake()
    {
        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }


    public void DetectHit()
    {
        hitBox.enabled = true;

        // ******* HERE - DETECT IF THE PLAYER IS IN THE HIT BOX *******8
        if (hitBox.)
    }
}

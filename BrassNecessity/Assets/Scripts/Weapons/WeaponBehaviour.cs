using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private LaserSeekBehaviour laserSeeking;


    public void FireLaser()
    {
        laserSeeking.SeekTarget();
    }

    public void ReleaseLaser()
    {
        laserSeeking.FinishSeeking();
    }

}

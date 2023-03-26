using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackersTracker : MonoBehaviour
{
    // This is attached to the player to track the number of enemies attacking the player at any given moment.
    public int numOfAttackers;
    public int maxAttackers = 2;   // Could be updated dynamically in the game


    public bool IsOpenToAttack
    {
        get
        {
            if (numOfAttackers < maxAttackers)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}

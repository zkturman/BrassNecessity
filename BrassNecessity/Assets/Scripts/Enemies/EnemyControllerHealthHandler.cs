using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerHealthHandler : EnemyHealthHandler
{
    EnemyController enemyController;


    private new void Awake()
    {
        base.Awake();
        enemyController = GetComponent<EnemyController>();
        IsTakingDamage = false;
    }

    public override void DamageEnemy(float damageAmount)
    {
        if (IsDead)
        {
            // Prevents the enemy still getting repeatedly hit while the death animation is playing.            
            return;
        }
        
        
        if (!IsTakingDamage)
        {
            // This is the first frame of the enemy being hit by the laser, so update the EnemyController
            enemyController.LaserContactBegins();
            IsTakingDamage = true;
        }
        
        Health -= damageAmount;
        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;
            enemyController.EnemyHasDied();
        }
    }

    public override void StopDamagingEnemy()
    {
        // If 'IsDead' then the enemy has already moved out of the 'being hit' state.
        if (!IsDead)
        {
            IsTakingDamage = false;
            enemyController.LaserContactEnds();
        }
    }



}

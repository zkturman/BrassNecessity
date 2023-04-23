using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField]
    protected float baseHealth = 80f;
    public float Health { get; protected set; }
    public bool IsDead { get; protected set; }

    protected void Awake()
    {
        IsDead = false;
        Health = baseHealth;
    }

    public float GetBaseHealth()
    {
        return baseHealth;
    }

    public virtual void DamageEnemy(float damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0)
        {
            IsDead = true;
        }
    }
}

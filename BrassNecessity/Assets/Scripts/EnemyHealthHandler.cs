using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField]
    protected float baseHealth = 80f;
    protected float health;
    public bool IsDead { get; protected set; }

    protected void Awake()
    {
        IsDead = false;
        health = baseHealth;
    }

    public virtual void DamageEnemy(float damageAmount)
    {
        baseHealth -= damageAmount;
        if (baseHealth < 0)
        {
            IsDead = true;
        }
    }
}

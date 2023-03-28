using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField]
    private float baseHealth = 250f;
    public float Health { get; private set; }

    private void Awake()
    {
        Health = baseHealth;
    }
    public void DamagePlayer(float damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0f)
        {
            Debug.Log("Game over.");
        }
    }

    public void HealPlayer(float healAmount)
    {
        Health += healAmount;
        if (Health > baseHealth)
        {
            Health = baseHealth;
        }
    }
}

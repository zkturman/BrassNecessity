using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHealth : EnemyHealthHandler
{
    [SerializeField]
    private float respawnTimeInSeconds = 3f;
    public override void DamageEnemy(float damageAmount)
    {
        base.DamageEnemy(damageAmount);
        StartCoroutine(hideEnemy());
    }

    private IEnumerator hideEnemy()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();
        mesh.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(respawnTimeInSeconds);
        mesh.enabled = true;
        collider.enabled = true;
        health = baseHealth;
        IsDead = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageOverTime : MonoBehaviour, IPlayerDamage
{
    [SerializeField]
    private float damagerPerSecond = 10f;
    [SerializeField]
    private float damageRadius = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DamagePlayer();
    }

    public void DamagePlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, damageRadius);
        PlayerHealthHandler playerHealth;
        float totalDamage = damagerPerSecond * Time.deltaTime;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out playerHealth))
            {
                Debug.Log("hit player");
                playerHealth.DamagePlayer(totalDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

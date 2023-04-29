using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyHealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private EnemyHealthHandler enemyHealth;
    [SerializeField]
    private TextMeshProUGUI healthLabel;

    private void Awake()
    {
        //healthLabel.text = enemyHealth.GetBaseHealth().ToString("F2");
        healthLabel.text = enemyHealth.GetBaseHealth().ToString("F0");
    }

    private void Update()
    {
        // Updated so that the numbers disappear when the enemy is killed
        if (enemyHealth.Health > 0)
        {
            healthLabel.text = enemyHealth.Health.ToString("F0");
        } else
        {
            healthLabel.text = "";
        }
    }
}

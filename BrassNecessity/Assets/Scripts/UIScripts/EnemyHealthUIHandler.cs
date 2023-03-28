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
        healthLabel.text = enemyHealth.GetBaseHealth().ToString("F2");
    }

    private void Update()
    {
        healthLabel.text = enemyHealth.Health.ToString("F2");
    }
}

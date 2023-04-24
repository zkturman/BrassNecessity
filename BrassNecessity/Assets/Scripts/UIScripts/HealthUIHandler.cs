using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthHandler playerHealth;
    private Label healthLabel;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        healthLabel = root.Q<Label>("HealthValue");
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealthHandler>();
        }
    }

    private void Update()
    {
        healthLabel.text = playerHealth.Health.ToString("F2");
    }
}

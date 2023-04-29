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
            DropItem();
        }
    }

    public virtual void DropItem()
    {
        ElementComponent enemyElement = findEnemyElement();
        ItemPickup dropItem;
        if (enemyElement != null)
        {
            dropItem = PickupItemFactory.TryCreateDropItem(enemyElement);
        }
        else
        {
            dropItem = PickupItemFactory.TryCreateDropItem();
        }
        if (dropItem != null)
        {
            dropItem.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    protected virtual ElementComponent findEnemyElement()
    {
        ElementComponent enemyElement = GetComponent<ElementComponent>();
        if (enemyElement == null)
        {
            enemyElement = GetComponentInChildren<ElementComponent>();
        }
        if (enemyElement == null)
        {
            enemyElement = GetComponentInParent<ElementComponent>();
        }
        return enemyElement;
    }
}

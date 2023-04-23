using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemFactory : MonoBehaviour
{
    [SerializeField]
    private float defaultChanceToDropItem = 0.8f;
    
    [SerializeField]
    private float defaultChanceItemIsShard = 0.6f;

    [SerializeField]
    private float defaultChanceHealthIsSmall = 0.6f;
    
    [SerializeField]
    private float defaultChanceHealthIsMedium = 0.3f;

    [SerializeField]
    private ElementPickup elementShard;

    [SerializeField]
    private HealthPickup smallHealthContainer;

    [SerializeField]
    private HealthPickup mediumeHealthContainer;

    [SerializeField]
    private HealthPickup largeHealthContainer;

    private static PickupItemFactory factorySingleton;

    private void Awake()
    {
        if (factorySingleton = null)
        {
            factorySingleton = this;
        }
    }

    public static IItemPickup TryCreateDropItem(ElementComponent sourceOfPickup)
    {
        return TryCreateDropItem(sourceOfPickup, factorySingleton.defaultChanceToDropItem, factorySingleton.defaultChanceItemIsShard);
    }

    public static IItemPickup TryCreateDropItem(ElementComponent sourceOfPickup, float chanceToDropItem, float chanceItemIsShard)
    {
        IItemPickup dropItem = null;
        float diceRoll = Random.Range(0f, 1f);
        if (diceRoll <= chanceToDropItem)
        {
            dropItem = factorySingleton.generatePickupItem(sourceOfPickup, chanceItemIsShard);
        }
        return dropItem;
    }

    private IItemPickup generatePickupItem(ElementComponent sourceOfPickup, float chanceItemIsShard)
    {
        float diceRoll = Random.Range(0f, 1f);
        IItemPickup droppedItem;
        if (diceRoll < chanceItemIsShard)
        {
            droppedItem = generateElementShard(sourceOfPickup);
        }
        else
        {
            droppedItem = generateHealthContainer();
        }
        return droppedItem;
    }

    private ElementPickup generateElementShard(ElementComponent sourceOfPickup)
    {
        Element.Type shardType = sourceOfPickup.ElementInfo.Primary;
        ElementPickup droppedElementShard = GameObject.Instantiate(elementShard);
        droppedElementShard.Element.SwitchType(shardType);
        return droppedElementShard;
    }

    private HealthPickup generateHealthContainer()
    {
        float diceRoll = Random.Range(0f, 1f);
        HealthPickup droppedHealthContainer;
        if (diceRoll < defaultChanceHealthIsSmall)
        {
            droppedHealthContainer = smallHealthContainer;
        }
        else if (diceRoll < defaultChanceHealthIsSmall + defaultChanceHealthIsMedium){
            droppedHealthContainer = mediumeHealthContainer;
        }
        else
        {
            droppedHealthContainer = largeHealthContainer;
        }
        return GameObject.Instantiate(droppedHealthContainer);
    }
}

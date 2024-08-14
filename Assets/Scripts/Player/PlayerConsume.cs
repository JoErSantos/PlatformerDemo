using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsume : MonoBehaviour
{
    private PlayerStats playerStats;
    private FlashSistem flashManager;

    void Start ()
    {
        playerStats = GetComponent<PlayerStats>();
        flashManager = GetComponent<FlashSistem>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if (collider.gameObject.layer == 8)
        {
            Consumable consumable = collider.gameObject.GetComponent<Consumable>();
            if(consumable.givesLive && playerStats.getHP() < 3 && !consumable.wasConsumed)
            {
                flashManager.Flash(1,0.25f);
                playerStats.gainHP();
                ConsumeConsumable(consumable);
            }
            else if(!consumable.givesLive)
            {
                ConsumeConsumable(consumable);
            }
        }
    }

    private void ConsumeConsumable(Consumable consumable)
    {
        consumable.wasConsumed = true;
        playerStats.addPoints(consumable.points);
        if(consumable.isRespawnable)
        {
            StartCoroutine(RespawnConsumable(consumable));
        }
        consumable.consume();
    }

    IEnumerator RespawnConsumable(Consumable consumable)
    {
        yield return new WaitForSeconds(15f);
        consumable.gameObject.SetActive(true);
        consumable.wasConsumed = false;
        consumable.PlayRespawnAnimation();
    }
}

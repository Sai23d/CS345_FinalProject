using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 30; // Or any other value you deem appropriate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.RecoverHealth(healthAmount);
                Destroy(gameObject); // Destroy the health pickup after collection
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;

    private void Awake()
    {
        this.currentHealth = this.maxHealth;

    }


    public void TakeDamage(int amount)
    {

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died");
        this.currentHealth = this.  maxHealth;
    }
}

using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour, IEnemy

{
    private float currentHealth, power, defense;


    public float maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void PerformAttack()
    {
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}

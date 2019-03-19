using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get ; set; }


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        animator.SetTrigger("BaseAttack");
        Debug.Log("Sword Attack");
    }

    public void PerformSpecialAttack() {
        animator.SetTrigger("SpecialAttack");
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hit" + other.name);
            // take the InemyComponent then apply damage for testing purpose I hard coded the value
            other.GetComponent<IEnemy>().TakeDamage(10);
        }
    }


}

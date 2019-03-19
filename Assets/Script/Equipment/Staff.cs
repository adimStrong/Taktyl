using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon,IProjectTiles
{
    private Animator animator;
    public List<BaseStat> Stats { get ; set; }
    public Transform projectilePoint { get ; set ; }

    FireBall fireBall;

    void Start()
    {
        animator = GetComponent<Animator>();

        fireBall = Resources.Load<FireBall>("Projecttile/FireBall");
    }

    public void PerformAttack()
    {

        animator.SetTrigger("BaseAttack");
        Debug.Log("staff Attack");
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

    public void CastProjectile()
    {
        FireBall fireballInstance = (FireBall)Instantiate(fireBall, projectilePoint.position, projectilePoint.rotation);
        fireballInstance.Direction = projectilePoint.forward;
    }
}

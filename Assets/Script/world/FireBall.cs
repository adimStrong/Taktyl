using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }

    Vector3 spawnPosition;

    private void Start()
    {
        Damage = 25;
        Range = 20f;
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * 500);
    }


    private void Update()
    {
        if(Vector3.Distance(spawnPosition,transform.position) >= Range)
        {

            Estinguish();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<IEnemy>().TakeDamage(Damage);
        }

        Estinguish();
    }

    void Estinguish()
    {
        Destroy(gameObject);
    }
}

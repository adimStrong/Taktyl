using UnityEngine;
using UnityEditor;

public interface IEnemy 
{
    void TakeDamage(int amount);
    void PerformAttack();

}

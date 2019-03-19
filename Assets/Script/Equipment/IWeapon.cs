
using System.Collections.Generic;


public interface IWeapon
{
    // an interface is a way to define like a contract and implements it in each class
    List<BaseStat> Stats { get; set; }
    void PerformAttack();
    void PerformSpecialAttack();

}

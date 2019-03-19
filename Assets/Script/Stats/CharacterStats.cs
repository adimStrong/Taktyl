using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // this class will control the stat of the player

    public List<BaseStat> stats = new List<BaseStat>();


    private void Start()
    {
        stats.Add(new BaseStat(4, "Power", "The Character Power Current Power Level"));

    }

    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            //getting the stat from the list of base stat then if find add the base value of the stat bonus to the baseStat addStatBonus to get the total final value with equipment
            stats.Find(x => x.StatName == statBonus.StatName).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }


    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            // same as addstat bonus in this class but we will remove the staff if we un equip an equipment
            stats.Find(x => x.StatName == statBonus.StatName).RemovingStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}

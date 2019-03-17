using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    // list of additional stat for equip item
    public List<StatBonus> baseStatBonus { get; set; }

    // value for base stat value
    public int BaseValue { get; set; }

    // name of the stat
    public string StatName { get; set; }

    // description of the stat like what is Str for
    public string StatDescription { get; set; }

    // final value of stats
    public int FinalValue { get; set; }


    public BaseStat(int baseValue, string statName, string statDescription)
    {
        this.baseStatBonus = new List<StatBonus>();

        this.BaseValue = BaseValue;
        this.StatName = statName;
        this.StatDescription = statDescription;

    }


    public void AddStatBonus(StatBonus statBonus)
    {
        this.baseStatBonus.Add(statBonus);

    }


    public void RemovingStatBonus(StatBonus statBonus)
    {
        this.baseStatBonus.Remove(statBonus);
    }

    public int GetCalculatedStatValue()
    {
        // we will take the final value and take the added bonus value to get the total value

        this.baseStatBonus.ForEach(x =>this.FinalValue += x.BonusValue);
        // then we will add the base stat
        FinalValue += BaseValue;
        return FinalValue;
    }
}

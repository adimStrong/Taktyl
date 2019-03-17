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

}

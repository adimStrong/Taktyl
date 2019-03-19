using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public List<BaseStat> Stats { get; set; }
    // something to define an item but not like an ID
    public string ObjectSlug { get; set; }

    public Item (List<BaseStat> stats, string objectSlug)
    {
        this.Stats = stats;
        this.ObjectSlug = objectSlug;
    }
}

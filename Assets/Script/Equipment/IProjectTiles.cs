using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectTiles 
{
    Transform projectilePoint { get; set; }
    void CastProjectile();
}

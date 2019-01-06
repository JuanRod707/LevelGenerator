using System.Collections.Generic;
using UnityEngine;

namespace JuanRod.LevelGenerator.Code
{
    public class Bounds : MonoBehaviour
    {
            public IEnumerable<Collider> Colliders => GetComponentsInChildren<Collider>();
    }
}

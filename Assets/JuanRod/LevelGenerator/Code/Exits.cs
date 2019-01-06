using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JuanRod.LevelGenerator.Code
{
    public class Exits : MonoBehaviour
    {
        public IEnumerable<Transform> ExitSpots => GetComponentsInChildren<Transform>().Where(t => t != transform);
    }
}

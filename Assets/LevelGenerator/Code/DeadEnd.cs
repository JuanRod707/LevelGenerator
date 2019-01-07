using UnityEngine;

namespace LevelGenerator.Code
{
    public class DeadEnd : MonoBehaviour
    {
        /// <summary>
        /// Bounds node in hierarchy
        /// </summary>
        public Bounds Bounds;

        public void Initialize(Level level)
        {
            transform.SetParent(level.transform);
            level.RegistrerNewDeadEnd(Bounds.Colliders);
        }
    }
}
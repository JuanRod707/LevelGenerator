using UnityEngine;

namespace LevelGenerator.Scripts
{
    public class DeadEnd : MonoBehaviour
    {
        /// <summary>
        /// Bounds node in hierarchy
        /// </summary>
        public Bounds Bounds;

        public void Initialize(Generator generator)
        {
            transform.SetParent(generator.transform);
            generator.RegistrerNewDeadEnd(Bounds.Colliders);
        }
    }
}
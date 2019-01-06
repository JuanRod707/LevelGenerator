using UnityEngine;

namespace JuanRod.Common
{
    public class AutoDispose : MonoBehaviour
    {
        public float Lifetime;

        void Start() => Destroy(gameObject, Lifetime);
    }
}

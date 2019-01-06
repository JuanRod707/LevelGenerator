using UnityEngine;

namespace JuanRod.Common
{
    public static class RandomService
    {
        public static bool RollD100(int chance) => Random.Range(1, 101) <= chance;

        public static int GetRandom(int min, int max) => Random.Range(min, max);

        public static float GetRandom(float min, float max) => Random.Range(min, max);
    }
}

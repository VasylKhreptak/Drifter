using System;

namespace Extensions
{
    public static class Mathf
    {
        public static void Probability(float probability, Action action)
        {
            if (Probability(probability))
            {
                action?.Invoke();
            }
        }

        public static bool Probability(float probability)
        {
            return probability >= UnityEngine.Random.value;
        }
    }
}

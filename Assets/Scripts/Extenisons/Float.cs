using UnityEngine;

namespace Extenisons
{
    public static class Float
    {
        public static float WithSign(this float value, int sign)
        {
            return UnityEngine.Mathf.Abs(value) * UnityEngine.Mathf.Sign(sign);
        }
        
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}

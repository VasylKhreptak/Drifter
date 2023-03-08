using UnityEngine;

namespace Extenisons
{
    public static class Float
    {
        public static float WithSign(this float value, int sign)
        {
            return Mathf.Abs(value) * Mathf.Sign(sign);
        }
    }
}

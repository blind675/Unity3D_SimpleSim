using UnityEngine;

namespace UtilMethods
{
    public static class Extensions
    {

        public static float Truncate(this float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }
    }
}

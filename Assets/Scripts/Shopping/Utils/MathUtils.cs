using UnityEngine;

namespace Shopping.Utils
{
    public static class MathUtils
    {
        public static Vector2 Clamp(Vector2 value, float min, float max)
        {
            value.x = Mathf.Clamp(value.x, min, max);
            value.y = Mathf.Clamp(value.y, min, max);
            return value;
        }

        public static Vector2 ClampAbsoluteValue(Vector2 value, float min, float max)
        {
            value.x = Mathf.Sign(value.x) * Mathf.Clamp(Mathf.Abs(value.x), min, max);
            value.y = Mathf.Sign(value.y) * Mathf.Clamp(Mathf.Abs(value.y), min, max);
            return value;
        }

        public static Vector3 DivideComponents(Vector3 v1, Vector3 v2)
        {
            v1.x /= v2.x;
            v1.y /= v2.y;
            v1.z /= v2.z;
            return v1;
        }

        public static Vector3 MultiplyComponents(Vector3 v1, Vector3 v2)
        {
            v1.x *= v2.x;
            v1.y *= v2.y;
            v1.z *= v2.z;
            return v1;
        }
    }
}
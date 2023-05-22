using UnityEngine;

namespace Utils
{
    public static class UnityExtensions
    {
        public static Vector2 XY(this Vector3 vector) => new Vector2(vector.x, vector.y);
        public static Vector2 XZ(this Vector3 vector) => new Vector2(vector.x, vector.z);
    }
}
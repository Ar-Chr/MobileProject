using UnityEngine;

namespace Ball.Enemies
{
    static class Targeting
    {
        public static Vector3? TryGetTarget(Vector3 origin, float projectileSpeed, Vector3 targetPosition, Vector3 targetVelocity)
        {
            Vector3 toPlayer = targetPosition - origin;

            float a = targetVelocity.sqrMagnitude - projectileSpeed * projectileSpeed;
            float b = 2 * Vector3.Dot(targetVelocity, toPlayer);
            float c = toPlayer.sqrMagnitude;

            float D = b * b - 4 * a * c;
            
            if (D < 0)
                return null;

            float sqrtD = Mathf.Sqrt(D);
            float t0 = (-b - sqrtD) / (2 * a);
            float t1 = (-b + sqrtD) / (2 * a);

            float t = t0 >= 0 ? t0 : t1 >= 0 ? t1 : -1;

            if (t < 0)
                return null;

            return targetPosition + targetVelocity * t;
        }
    }
}

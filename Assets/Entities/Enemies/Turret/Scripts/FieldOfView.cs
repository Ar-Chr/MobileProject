using System;
using UnityEngine;

namespace Ball.Shared
{
    [Serializable]
    class FieldOfView
    {
        [SerializeField] Transform viewOrigin;
        [Space]
        [SerializeField] float viewAngle;
        [SerializeField] float maxRange;

        public bool InView(Vector3 point)
        {
            Vector3 toPlayer = point - viewOrigin.position;
            float angle = Mathf.Acos(Vector3.Dot(toPlayer, viewOrigin.forward) / toPlayer.magnitude);
            return angle * Mathf.Rad2Deg <= viewAngle;
        }

        public bool InRange(Vector3 point)
        {
            return (point - viewOrigin.position)
                .sqrMagnitude <= maxRange * maxRange;
        }

        public void OnDrawGizmosSelected()
        {
            float viewAngleRad = viewAngle * Mathf.Deg2Rad;

            float sin = Mathf.Sin(viewAngleRad) * maxRange;
            float cos = Mathf.Cos(viewAngleRad) * maxRange;

            Vector3 up = viewOrigin.TransformPoint(0, sin, cos);
            Vector3 down = viewOrigin.TransformPoint(0, -sin, cos);
            Vector3 left = viewOrigin.TransformPoint(-sin, 0, cos);
            Vector3 right = viewOrigin.TransformPoint(sin, 0, cos);            

            Gizmos.color = Color.red;
            Gizmos.DrawLine(viewOrigin.position, up);
            Gizmos.DrawLine(viewOrigin.position, down);
            Gizmos.DrawLine(viewOrigin.position, left);
            Gizmos.DrawLine(viewOrigin.position, right);
            Gizmos.DrawLine(viewOrigin.position, viewOrigin.position + viewOrigin.forward * maxRange);
        }
    }
}

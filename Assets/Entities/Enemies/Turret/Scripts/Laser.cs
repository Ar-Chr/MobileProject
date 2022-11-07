using UnityEngine;

namespace Ball.Enemies
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] float startWidth;
        [SerializeField] float endWidth;
        [SerializeField] Color startColor;
        [SerializeField] Color endColor;
        [Space]
        [SerializeField] Transform origin;
        [Space]
        [SerializeField] Turret turret;
        [SerializeField] LineRenderer lineRenderer;

        private void Reset()
        {
            if (!lineRenderer)
                lineRenderer = GetComponent<LineRenderer>();
        }

        private void Awake()
        {
            turret.Shooter.ChargeChanged += HandleChargeChanged;
        }

        private void Start()
        {
            lineRenderer.startWidth = startWidth;
            lineRenderer.endWidth = endWidth;

            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;
        }

        private void Update()
        {
            Vector3 laserEnd = origin.position + origin.forward * 40f;

            if (!Physics.Linecast(origin.position, laserEnd, out RaycastHit hit))
                hit.point = laserEnd;

            SetPositions(origin.position, hit.point);
            distancePercentage = (hit.point - origin.position).magnitude / 40f;
        }

        float chargePercentage;
        float distancePercentage;

        private void LateUpdate()
        {
            float widthAtMaxRange = Mathf.Lerp(endWidth, startWidth, chargePercentage);
            lineRenderer.endWidth = widthAtMaxRange * distancePercentage + startWidth * (1 - distancePercentage);
        }

        void SetPositions(params Vector3[] positions) => lineRenderer.SetPositions(positions);

        private void HandleChargeChanged(float chargePercentage)
        {
            lineRenderer.endColor = Color.Lerp(endColor, startColor, chargePercentage);
            this.chargePercentage = chargePercentage;
        }
    }
}

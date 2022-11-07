using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball
{
    public class LavaMovement : MonoBehaviour
    {
        [SerializeField] bool verticalMovement;
        [SerializeField] float verticalMovementFrequency;
        [SerializeField] float verticalMovementAmplitude;

        [SerializeField] bool horizontalMovement;
        [SerializeField] float horizontalMovementSpeed;

        float initialY;

        private void Start()
        {
            initialY = transform.position.y;
        }

        private void Update()
        {
            if (verticalMovement)
            {
                float sine = Mathf.Sin(Time.time * 2 * Mathf.PI * verticalMovementFrequency);
                float offset = sine * verticalMovementAmplitude;
                transform.position = transform.position.WithY(initialY + offset);
            }
                
            if (horizontalMovement)
            {
                transform.position += horizontalMovementSpeed * Time.deltaTime * Vector3.forward;
            }
        }
    }
}

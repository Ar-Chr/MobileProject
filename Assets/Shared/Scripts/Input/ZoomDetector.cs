using UnityEngine;

public class ZoomDetector : MonoBehaviour
{
    [SerializeField] bool oneTouchDebugMode;
    [SerializeField] Vector2 debugTouchPosition;

    Vector2 touchOnePosition => Input.GetTouch(0).position;
    Vector2 touchTwoPosition => oneTouchDebugMode ?
                                       debugTouchPosition :
                                       Input.GetTouch(1).position;

    float startingSqrDistance;

    bool detecting;

    void Update()
    {
        if (Input.touchCount == 2 ||
            (oneTouchDebugMode && Input.touchCount == 1))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began &&
                (oneTouchDebugMode ||
                Input.GetTouch(1).phase == TouchPhase.Began))
                StartDetection();

            if (detecting)
                Debug.Log($"Zoom: {GetZoomLevel()}x");
        }
        else
        {
            detecting = false;
        }
    }

    void StartDetection()
    {
        detecting = true;

        startingSqrDistance = (touchOnePosition - touchTwoPosition).sqrMagnitude;
    }

    float GetZoomLevel()
    {
        return (touchOnePosition - touchTwoPosition).sqrMagnitude / startingSqrDistance;
    }
}
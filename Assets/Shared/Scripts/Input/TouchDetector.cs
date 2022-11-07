using System;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField] float tapThresholdMilliseconds;

    public static event Action<bool> TouchStateChanged;
    public static event Action<Vector2> PositionChanged;
    public static event Action TapDetected;

    float timeSinceTouchStart;

#if UNITY_STANDALONE_WIN

    Vector3 prevPosition;
    bool mouseDown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseDown = true;
            prevPosition = Input.mousePosition;
            timeSinceTouchStart = 0;
            TouchStateChanged?.Invoke(true);
        }

        if (mouseDown)
        {
            timeSinceTouchStart += Time.deltaTime;
            PositionChanged?.Invoke(Input.mousePosition - prevPosition);
            prevPosition = Input.mousePosition;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            mouseDown = false;
            if (timeSinceTouchStart < tapThresholdMilliseconds / 1000)
                TapDetected?.Invoke();
            TouchStateChanged?.Invoke(false);
        }
    }

#else

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                timeSinceTouchStart = 0;
                TouchStateChanged?.Invoke(true);
                break;

            case TouchPhase.Stationary:
                timeSinceTouchStart += Time.deltaTime;
                break;

            case TouchPhase.Moved:
                timeSinceTouchStart += Time.deltaTime;
                PositionChanged?.Invoke(touch.deltaPosition);
                break;

            case TouchPhase.Ended:
                if (timeSinceTouchStart < tapThresholdMilliseconds / 1000)
                    TapDetected?.Invoke();

                TouchStateChanged?.Invoke(false);
                break;
        }
    }
    
#endif

    void LogStateChange(bool state)
    {
        Debug.Log(state ? "Touch started" : "Touch ended");
    }

    void LogPositionChange(Vector2 deltaPosition)
    {
        Debug.Log(deltaPosition);
    }
}
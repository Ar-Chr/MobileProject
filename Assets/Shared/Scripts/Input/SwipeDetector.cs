using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] [Min(0)] float minMainDelta;
    [SerializeField] [Min(0)] float maxOtherDelta;
    [SerializeField] [Min(0)] float angleCheckThreshold;

    public static event Action<SwipeDirections> SwipeDetected;

    float maxDeltaRatio => maxOtherDelta / minMainDelta;

    SwipeAxis x;
    SwipeAxis y;

    Vector2 origin;
    Vector2 totalDelta;

    bool detectingVertical;
    bool detectingHorizontal;

    void Start()
    {
        x = new SwipeAxis(v => v.x, (vector, value) => new Vector2(value, vector.y), SwipeRight, SwipeLeft);
        y = new SwipeAxis(v => v.y, (vector, value) => new Vector2(vector.x, value), SwipeUp, SwipeDown);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                BeginDetection(touch);

            totalDelta += touch.deltaPosition;

            CheckHorizontalDirectionChange(touch);
            CheckVerticalDirectionChange(touch);

            if (detectingHorizontal)
            {
                CheckHorizontalSwipe();
            }
            if (detectingVertical)
            {
                CheckVerticalSwipe();
            }
        }
        else
        {
            detectingHorizontal = false;
            detectingVertical = false;
        }
    }

    void BeginDetection(Touch touch)
    {
        detectingHorizontal = true;
        detectingVertical = true;
        origin = touch.position;
        totalDelta = Vector2.zero;
    }

    void CheckHorizontalDirectionChange(Touch touch) =>
       CheckDirectionChange(touch, x);

    void CheckVerticalDirectionChange(Touch touch) =>
       CheckDirectionChange(touch, y);

    void CheckDirectionChange(Touch touch, SwipeAxis axis)
    {
        float deltaPosition = axis.Get(touch.deltaPosition);

        if (deltaPosition != 0 &&
            Mathf.Sign(deltaPosition) != Mathf.Sign(axis.Get(totalDelta)))
        {
            origin = axis.WithCoord(origin, axis.Get(touch.position) - deltaPosition);
            totalDelta.x = touch.deltaPosition.x;
        }
    }

    void CheckHorizontalSwipe() =>
       CheckSwipe(x, y, CancelHorizontal);

    void CheckVerticalSwipe() =>
       CheckSwipe(y, x, CancelVertical);

    void CheckSwipe(SwipeAxis mainAxis, SwipeAxis otherAxis, Action cancellationMethod)
    {
        float totalMainDelta = mainAxis.Get(totalDelta);

        if (-angleCheckThreshold < totalMainDelta && totalMainDelta < angleCheckThreshold)
            return;

        if (!DeltaRatioLowEnough(totalMainDelta, otherAxis.Get(totalDelta)))
        {
            cancellationMethod();
            return;
        }

        if (totalMainDelta >= minMainDelta)
        {
            CancelHorizontal();
            CancelVertical();
            mainAxis.DetectedPositive();
        }
        else if (totalMainDelta <= -minMainDelta)
        {
            CancelHorizontal();
            CancelVertical();
            mainAxis.DetectedNegative();
        }
    }

    bool DeltaRatioLowEnough(float mainDelta, float otherDelta)
    {
        if (otherDelta == 0)
            return true;

        if (mainDelta == 0)
            return false;

        return Mathf.Abs(otherDelta / mainDelta) < maxDeltaRatio;
    }

    void SwipeLeft()
    {
        SwipeDetected(SwipeDirections.Left);
    }

    void SwipeRight()
    {
        SwipeDetected(SwipeDirections.Right);
    }

    void SwipeUp()
    {
        SwipeDetected(SwipeDirections.Up);
    }

    void SwipeDown()
    {
        SwipeDetected(SwipeDirections.Down);
    }

    void CancelHorizontal()
    {
        detectingHorizontal = false;
    }

    void CancelVertical()
    {
        detectingVertical = false;
    }

    class SwipeAxis
    {
        Func<Vector2, float> getCoordinate;
        Func<Vector2, float, Vector2> withCoordinate;
        Action detectedPositive;
        Action detectedNegative;

        public SwipeAxis(Func<Vector2, float> getCoordinate,
                         Func<Vector2, float, Vector2> withCoordinate,
                         Action detectedPositive,
                         Action detectedNegative)
        {
            this.getCoordinate = getCoordinate;
            this.withCoordinate = withCoordinate;
            this.detectedPositive = detectedPositive;
            this.detectedNegative = detectedNegative;
        }

        public float Get(Vector2 vector) => getCoordinate(vector);

        public Vector2 WithCoord(Vector2 vector, float value) => withCoordinate(vector, value);

        public void DetectedPositive() => detectedPositive();

        public void DetectedNegative() => detectedNegative();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputController : MonoBehaviour
{
    public float _xThreshold = 0.5f;
    public float _yThreshold = 0.5f;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        TouchSwipe();
#else
		MouseSwipe();
#endif
    }

    public void MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            CalculateDirection(currentSwipe);
        }
    }

    public void TouchSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                Vector2 currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                CalculateDirection(currentSwipe);
            }
        }
    }

    private void CalculateDirection(Vector2 currentSwipe)
    {
        //swipe up
        if (currentSwipe.y > 0 && currentSwipe.x > -_xThreshold & currentSwipe.x < _xThreshold)
        {
            EventBus.RaiseSwipeUp(this);
        }
        //swipe down
        if (currentSwipe.y < 0 && currentSwipe.x > -_xThreshold & currentSwipe.x < _xThreshold)
        {
            EventBus.RaiseSwipeDown(this);
        }
        //swipe left
        if (currentSwipe.x < 0 && currentSwipe.y > -_yThreshold & currentSwipe.y < _yThreshold)
        {
            EventBus.RaiseSwipeLeft(this);
        }
        //swipe right
        if (currentSwipe.x > 0 && currentSwipe.y > -_yThreshold & currentSwipe.y < _yThreshold)
        {
            EventBus.RaiseSwipeRight(this);
        }
    }
}

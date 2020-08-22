using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    private bool _isClicked = false;

    private void Start()
    {
        EventBus.OnSwipeUp += OnSwipeUp;
        EventBus.OnSwipeDown += OnSwipeDown;
        EventBus.OnSwipeLeft += OnSwipeLeft;
        EventBus.OnSwipeRight += OnSwipeRight;
    }

    private void OnDestroy()
    {
        EventBus.OnSwipeUp -= OnSwipeUp;
        EventBus.OnSwipeDown -= OnSwipeDown;
        EventBus.OnSwipeLeft -= OnSwipeLeft;
        EventBus.OnSwipeRight -= OnSwipeRight;
    }

    private void OnSwipeUp(object sender, System.EventArgs e)
    {
        if(_isClicked)
        {
            Debug.Log(name + " - Swipe Up!");
            _isClicked = false;
        }
    }

    private void OnSwipeDown(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            Debug.Log(name + " - Swipe Down!");
            _isClicked = false;
        }
    }

    private void OnSwipeLeft(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            Debug.Log(name + " - Swipe Left!");
            _isClicked = false;
        }
    }

    private void OnSwipeRight(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            Debug.Log(name + " - Swipe Right!");
            _isClicked = false;
        }
    }

    void OnMouseDown()
    {
        _isClicked = true;
    }
}

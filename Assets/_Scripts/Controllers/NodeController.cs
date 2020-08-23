using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    private Color _nodeColor;
    public Color NodeColor
    {
        get { return _nodeColor; }
        set
        {
            _nodeColor = value;
            _nodeMaterial.SetColor("_BaseColor", _nodeColor);
        }
    }

    private bool _isClicked = false;
    private Material _nodeMaterial;

    public enum NodeMovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }


    private void Awake()
    {
        _nodeMaterial = GetComponent<MeshRenderer>().materials[0];

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
            _isClicked = false;
            EventBus.RaiseMoveNode(this, PosX, PosY, NodeMovementDirection.Up);
        }
    }

    private void OnSwipeDown(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            _isClicked = false;
            EventBus.RaiseMoveNode(this, PosX, PosY, NodeMovementDirection.Down);
        }
    }

    private void OnSwipeLeft(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            _isClicked = false;
            EventBus.RaiseMoveNode(this, PosX, PosY, NodeMovementDirection.Left);
        }
    }

    private void OnSwipeRight(object sender, System.EventArgs e)
    {
        if (_isClicked)
        {
            _isClicked = false;
            EventBus.RaiseMoveNode(this, PosX, PosY, NodeMovementDirection.Right);
        }
    }

    public void GoToNewPosition()
    {
        transform.localPosition = new Vector3(PosX * 1f + 0.5f, -PosY * 1f - 0.5f, 0f);
    }

    void OnMouseDown()
    {
        _isClicked = true;
    }
}

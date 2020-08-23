using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private float _movementTime = 1;

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
    private Material _nodeMaterial = default;
    private float _movementIterator = 0;
    private bool _isInteractable = true;

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
        EventBus.OnMoveNode += OnMoveNode;
        EventBus.OnAllMovementsEnded += OnAllMovementsEnded;
    }

    private void OnDestroy()
    {
        EventBus.OnSwipeUp -= OnSwipeUp;
        EventBus.OnSwipeDown -= OnSwipeDown;
        EventBus.OnSwipeLeft -= OnSwipeLeft;
        EventBus.OnSwipeRight -= OnSwipeRight;
        EventBus.OnMoveNode -= OnMoveNode;
        EventBus.OnAllMovementsEnded -= OnAllMovementsEnded;
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

    public void GoToNewPosition(bool animate = false)
    {
        Vector3 desiredPosition = new Vector3(PosX * 1f + 0.5f, -PosY * 1f - 0.5f, 0f);

        if (animate && gameObject.activeSelf)
        {
            StopAllCoroutines();
            StartCoroutine(Move(transform.localPosition, desiredPosition));
        }
        else
        {
            transform.localPosition = desiredPosition;
        }
    }

    private IEnumerator Move(Vector3 initialPos, Vector3 desiredPos)
    {
        _movementIterator = 0f;

        while (_movementIterator < 1f)
        {
            _movementIterator = Mathf.Clamp(_movementIterator + 1 / _movementTime * Time.deltaTime, 0f, 1f);
            transform.localPosition = Vector3.Lerp(initialPos, desiredPos, _movementIterator);
            yield return null;
        }

        EventBus.RaiseNodeMovementEnded(this);
    }

    private void OnMoveNode(object sender, EventArgs e)
    {
        _isInteractable = false;
    }

    private void OnAllMovementsEnded(object sender, EventArgs e)
    {
        _isInteractable = true;
    }

    void OnMouseDown()
    {
        if (_isInteractable)
        {
            _isClicked = true;
        }
    }
}

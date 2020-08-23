using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{

    #region Gameplay Events

    #region Swipe Up

    public static event EventHandler OnSwipeUp;

    public static void RaiseSwipeUp(object sender)
    {
        OnSwipeUp?.Invoke(sender, EventArgs.Empty);
    }

    #endregion

    #region Swipe Down

    public static event EventHandler OnSwipeDown;

    public static void RaiseSwipeDown(object sender)
    {
        OnSwipeDown?.Invoke(sender, EventArgs.Empty);
    }

    #endregion

    #region Swipe Left

    public static event EventHandler OnSwipeLeft;

    public static void RaiseSwipeLeft(object sender)
    {
        OnSwipeLeft?.Invoke(sender, EventArgs.Empty);
    }

    #endregion

    #region Swipe Right

    public static event EventHandler OnSwipeRight;

    public static void RaiseSwipeRight(object sender)
    {
        OnSwipeRight?.Invoke(sender, EventArgs.Empty);
    }

    #endregion

    #region Move Node

    public class MoveNodeEventArgs : EventArgs
    {

        public readonly int PosX;
        public readonly int PosY;
        public NodeController.NodeMovementDirection MovementDirection;

        public MoveNodeEventArgs(int posX, int posY, NodeController.NodeMovementDirection movementDirection)
        {
            PosX = posX;
            PosY = posY;
            MovementDirection = movementDirection;
        }
    }

    public static event EventHandler OnMoveNode;

    public static void RaiseMoveNode(object sender, int posX, int posY, NodeController.NodeMovementDirection movementDirection)
    {
        OnMoveNode?.Invoke(sender, new MoveNodeEventArgs(posX, posY, movementDirection));
    }

    #endregion

    #endregion

    #region GUI Events

    #endregion

    #region Scene Management Events

    #endregion

}
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{

    #region Gameplay events

    #region Game Began

    public static event EventHandler GameBegan;

    public static void OnGameBegan(object sender)
    {
        if (GameBegan != null)
        {
            GameBegan(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Game Speed Increased

    public static event EventHandler GameSpeedIncreased;

    public static void OnGameSpeedIncreased(object sender)
    {
        if (GameSpeedIncreased != null)
        {
            GameSpeedIncreased(sender, EventArgs.Empty);
        }
    }

    #endregion

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

    #region Vehicle Collided

    public static event EventHandler VehicleCollided;

    public static void OnVehicleCollided(object sender)
    {
        if (VehicleCollided != null)
        {
            VehicleCollided(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Vehicle Exploded

    public class VehicleExplodedEventArgs : EventArgs
    {

        public readonly Vector3 ExplosionPosition;

        public VehicleExplodedEventArgs(Vector3 explosionPosition)
        {
            ExplosionPosition = explosionPosition;
        }
    }

    public static event EventHandler VehicleExploded;

    public static void OnVehicleExploded(object sender, Vector3 explosionPosition)
    {
        if (VehicleExploded != null)
        {
            VehicleExploded(sender, new VehicleExplodedEventArgs(explosionPosition));
        }
    }

    #endregion

    #region Danger Detected

    public class DangerDetectedEventArgs : EventArgs
    {

        public readonly List<int> SafePositions;

        public DangerDetectedEventArgs(List<int> safePositions)
        {
            SafePositions = safePositions;
        }
    }

    public static event EventHandler DangerDetected;

    public static void OnDangerDetected(object sender, List<int> safePositions)
    {
        if (DangerDetected != null)
        {
            DangerDetected(sender, new DangerDetectedEventArgs(safePositions));
        }
    }

    #endregion

    #endregion

    #region GUI events

    #region Play Button Touched

    public static event EventHandler PlayButtonTouched;

    public static void OnPlayButtonTouched(object sender)
    {
        if (PlayButtonTouched != null)
        {
            PlayButtonTouched(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Watch Button Touched

    public static event EventHandler WatchButtonTouched;

    public static void OnWatchButtonTouched(object sender)
    {
        if (WatchButtonTouched != null)
        {
            WatchButtonTouched(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Quit Gameplay Button Touched

    public static event EventHandler QuitGameplayButtonTouched;

    public static void OnQuitGameplayButtonTouched(object sender)
    {
        if (QuitGameplayButtonTouched != null)
        {
            QuitGameplayButtonTouched(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Game Loading Shown

    public static event EventHandler GameLoadingShown;

    public static void OnGameLoadingShown(object sender)
    {
        if (GameLoadingShown != null)
        {
            GameLoadingShown(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Game Loading Done

    public static event EventHandler GameLoadingDone;

    public static void OnGameLoadingDone(object sender)
    {
        if (GameLoadingDone != null)
        {
            GameLoadingDone(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Ai Loading Done

    public static event EventHandler AiLoadingDone;

    public static void OnAiLoadingDone(object sender)
    {
        if (AiLoadingDone != null)
        {
            AiLoadingDone(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Main Menu Loading Shown

    public static event EventHandler MainMenuLoadingShown;

    public static void OnMainMenuLoadingShown(object sender)
    {
        if (MainMenuLoadingShown != null)
        {
            MainMenuLoadingShown(sender, EventArgs.Empty);
        }
    }

    #endregion

    #endregion

    #region Scene Management events

    #region Game Scene Loaded

    public static event EventHandler GameSceneLoaded;

    public static void OnGameSceneLoaded(object sender)
    {
        if (GameSceneLoaded != null)
        {
            GameSceneLoaded(sender, EventArgs.Empty);
        }
    }

    #endregion

    #region Main Menu Scene Loaded

    public static event EventHandler MainMenuSceneLoaded;

    public static void OnMainMenuSceneLoaded(object sender)
    {
        if (MainMenuSceneLoaded != null)
        {
            MainMenuSceneLoaded(sender, EventArgs.Empty);
        }
    }

    #endregion

    #endregion

}
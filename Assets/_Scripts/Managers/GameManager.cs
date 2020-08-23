using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int Score { get; private set; }

    void Start()
    {
        EventBus.OnGameStarted += OnGameStarted;
        EventBus.OnNodeDestroyed += OnNodeDestroyed;
    }

    private void OnDestroy()
    {
        EventBus.OnGameStarted -= OnGameStarted;
        EventBus.OnNodeDestroyed -= OnNodeDestroyed;
    }

    private void OnGameStarted(object sender, System.EventArgs e)
    {
        Score = 0;
    }

    private void OnNodeDestroyed(object sender, System.EventArgs e)
    {
        Score++;
        EventBus.RaiseScoreUpdated(this);
    }
}

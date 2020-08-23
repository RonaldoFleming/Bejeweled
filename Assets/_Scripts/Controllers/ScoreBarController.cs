using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarController : MonoBehaviour
{
    private Text _scoreText = default;
    
    void Start()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = "0";
        EventBus.OnScoreUpdated += OnScoreUpdated;
    }

    private void OnDestroy()
    {
        EventBus.OnScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreUpdated(object sender, System.EventArgs e)
    {
        _scoreText.text = GameManager.Instance.Score.ToString();
    }
}

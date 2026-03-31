using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Color32 winColor;
    [SerializeField] private Color32 loseColor;
    private VisualElement _root;
    private Color32 _finalColor;
    private Label _winLoseLabel;
    private string _message;
    private Button _playAgainButton;

    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        
        SaveHightestScore();
        SetUIValues();
        _playAgainButton = _root.Q<Button>("Play");
        _playAgainButton.clicked += PlayAgain;
    }

    private void PlayAgain()
    {
        GameManager.Instance.PlayAgain();
    }

    private void OnDisable()
    {
        _playAgainButton.clicked -= PlayAgain;
    }

    private int GetHightestScore()
    {
        var score = PlayerPrefs.GetInt("HightestScore", 0);
        return score;
    }

    private void SaveHightestScore()
    {
        if (GetHightestScore() < GameManager.Instance.GetScore())
        {
            PlayerPrefs.SetInt("HightestScore", GameManager.Instance.GetScore());
        }
    }

    private void SetUIValues()
    {
        var scoreLabel = _root.Q<Label>("ActualScore");
        scoreLabel.text = GameManager.Instance.GetScore().ToString();
        var bestScoreLabel = _root.Q<Label>("ActualBestScore");
        bestScoreLabel.text = GetHightestScore().ToString();
        Debug.Log(GameManager.Instance.FinalStateMatch);
        switch (GameManager.Instance.FinalStateMatch)
        {
            case GameManager.FinalState.Win:
                _finalColor = winColor;
                _message = "You win!";
                break;
            case GameManager.FinalState.Lose:
                _finalColor = loseColor;
                _message = "You Lose!";
                break;
            default:
                break;
        }
        var messageLabel = _root.Q<Label>("Win");
        messageLabel.text = _message;
        messageLabel.style.color = new StyleColor(_finalColor);
    }
    
}

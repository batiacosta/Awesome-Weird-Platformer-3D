using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUI : MonoBehaviour
{
    private VisualElement _root;
    private Label _scoreLabel;
    private Label _heartsLabel;
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _scoreLabel = _root.Q<Label>("StarCounter");
        _heartsLabel = _root.Q<Label>("LivesCounter");
    }

    private void OnEnable()
    {
        GameManager.OnScoreChanges += UpdateScore;
        GameManager.OnHeartChanges += UpdateHearts;
        UpdateScore();
        UpdateHearts();
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanges -= UpdateScore;
        GameManager.OnHeartChanges -= UpdateHearts;
    }

    private void UpdateHearts()
    {
        _heartsLabel.text = GameManager.Instance.GetHearts().ToString();
    }

    private void UpdateScore()
    {
        _scoreLabel.text = GameManager.Instance.GetScore().ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private VisualElement _root;
    private Button _startButton;
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _startButton = _root.Q<Button>("PlayButton");
    }

    private void OnEnable()
    {
        var gameManager = FindObjectOfType<GameManager>();
        _startButton.clicked += gameManager.PlayGame;
    }

    private void OnDisable()
    {
        var gameManager = FindObjectOfType<GameManager>();
        _startButton.clicked -= gameManager.PlayGame;
    }
}

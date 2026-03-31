using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement _root;
    private Button _pauseButton;
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _pauseButton = _root.Q<Button>("ContinueButton");
    }

    private void OnEnable()
    {
        _pauseButton.clicked += Continue;
    }

    private void OnDisable()
    {
        _pauseButton.clicked -= Continue;
    }
    private void Continue()
    {
        var gameManager = GameManager.Instance;
        gameManager.PlayGame();
    }
}

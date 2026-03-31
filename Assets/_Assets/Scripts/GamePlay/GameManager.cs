using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnScoreChanges;
    public static event Action OnHeartChanges;

    public enum GameState
    {
        Menu,
        Playing,
        Pause,
        GameOver
    }

    public GameState CurrentGameState => _currentGameState;

    [SerializeField] private UIDocument mainMenuUI;
    [SerializeField] private UIDocument pauseMenuUI;
    [SerializeField] private UIDocument gameOverUI;
    [SerializeField] private UIDocument scoreUI;
    [SerializeField] private GameObject player;
    
    private GameState _currentGameState = GameState.Menu;

    private int hearts;
    private int score;

    //[SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        _currentGameState = GameState.Menu;
    }

    private void SetGameState(GameState newGameState)
    {
        _currentGameState = newGameState;
    }

    

    private void HideMainMenu() => mainMenuUI.gameObject.SetActive(false);
    private void HideGameOver() => gameOverUI.gameObject.SetActive(false);
    private void HidePauseMenu() => pauseMenuUI.gameObject.SetActive(false);
    private void HideScore() => scoreUI.gameObject.SetActive(false);
    
    public void ShowMainMenu()
    {
        mainMenuUI.gameObject.SetActive(false);
        HideScore();
    }

    public void PlayGame()
    {
        SetGameState(GameState.Playing);
        HideMainMenu();
        HideGameOver();
        HidePauseMenu();
        player.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        SetGameState(GameState.Pause);
        pauseMenuUI.gameObject.SetActive(true);
        HideScore();
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        gameOverUI.gameObject.SetActive(true);
        HideScore();
    }

    public void ResetScore()
    {
        hearts = 0;
        score = 0;
    }
    public void AddScore()
    {
        score++;
        OnScoreChanges?.Invoke();
    }
    public  void AddHearts()
    {
        hearts++;
        OnHeartChanges?.Invoke();
    }
    
    public int GetHearts() => hearts;
    public int GetScore() => score;
}
    
    

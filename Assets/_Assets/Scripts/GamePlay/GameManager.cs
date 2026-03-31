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
    [SerializeField] private GameObject player;
    
    private GameState _currentGameState = GameState.Menu;

    //[SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        _currentGameState = GameState.Menu;
    }

    private void SetGameState(GameState newGameState)
    {
        _currentGameState = newGameState;
    }

    public void ShowMainMenu()
    {
        mainMenuUI.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        SetGameState(GameState.Playing);
        HideMainMenu();
        HideGameOver();
        HidePauseMenu();
        player.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        SetGameState(GameState.Pause);
        pauseMenuUI.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        gameOverUI.gameObject.SetActive(true);
    }

    private void HideMainMenu() => mainMenuUI.gameObject.SetActive(false);
    private void HideGameOver() => gameOverUI.gameObject.SetActive(false);
    private void HidePauseMenu() => pauseMenuUI.gameObject.SetActive(false);
}
    
    

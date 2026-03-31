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
        mainMenuUI.enabled = true;
    }

    public void PlayGame()
    {
        SetGameState(GameState.Playing);
        HideMainMenu();
        player.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        SetGameState(GameState.Pause);
        
    }

    public void GameOver()
    {
        
    }

    private void HideMainMenu() => mainMenuUI.enabled = false;
    private void HideGameOver() => HideMainMenu();
}
    
    

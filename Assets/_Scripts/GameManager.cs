using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] triggers;
    public static GameManager Instance;
    [SerializeField] GameState StartState = GameState.StartMenu;
    [HideInInspector]
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // UpdateGameState(GameState.StartMenu);
        UpdateGameState(StartState);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.StartMenu:
                break;
            case GameState.GamePlay:
                HandleGamePlay();
                break;
            case GameState.EndGame:
                HandleEndGame();
                break;
                // default:
                //     Debug.LogError("No state!");
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log("Current state: " + newState.ToString());
    }

    private void HandleEndGame()
    {

        // foreach (GameObject g in triggers) g.SetActive(false);

        // throw new NotImplementedException();
        Debug.Log("HandleEndGame");
    }

    public void RestartLevel()
    {

    }
    private void HandleGamePlay()
    {
        // throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum GameState
{
    StartMenu,
    GamePlay,
    EndGame
}
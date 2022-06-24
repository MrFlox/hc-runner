using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject ingameMenu;

    void Awake()
    {
        GameManager.OnGameStateChanged += onStateChange;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= onStateChange;
    }

    private void onStateChange(GameState state)
    {
        gameMenu.SetActive(state == GameState.StartMenu);
        if (state == GameState.StartMenu)
            Debug.Log("Menu state!");
        ingameMenu.SetActive(state == GameState.GamePlay);
    }

    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{

    [SerializeField] bool isActive = true;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GameManager.OnGameStateChanged += onStateChange;
    }


    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= onStateChange;
    }


    private void onStateChange(GameState state)
    {
        // throw new NotImplementedException();
        gameObject.SetActive(state == GameState.GamePlay);
        // isActive = (state == GameState.GamePlay);
    }

    void OnTriggerEnter(Collider other)
    {
        // if (!isActive) return;
        // if (GameManager.Instance.State != GameState.GamePlay) return;
        if (other.tag == "runner")
        {
            gameObject.SetActive(false);
            PeoplesManager.instance.newLevel(gameObject);
            // GameManager.Instance.UpdateGameState(GameState.EndGame);
            // isActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class BallManager : NetworkBehaviour
{

    [Header("Elements")]
    [SerializeField] private Ball ballPrefab;

    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }



    void Update()
    {
        
    }

    private void OnDestroy()
    {
        base.OnDestroy();

        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.State gameState)
    {
        switch (gameState)
        {
            case GameManager.State.Game:
                SpawnBall();
                break;

                //case GameManager.State.
        }
    }

    private void SpawnBall()
    {
        if (!IsServer)
            return;

        //Ball ballInstance = Instantiate(ballPrefab, Vector2.up * 5, Quaternion.identity, transform); // apply the parent Ball Manager to Ball prefab. So ball is child of Ball Manager.
        Ball ballInstance = Instantiate(ballPrefab, Vector2.up * 5, Quaternion.identity); // spawn object locally. must spawn it locally first. Only host can see it here.
        ballInstance.GetComponent<NetworkObject>().Spawn(); // spawn object on the network. now both host and clients can see it.
    }

}

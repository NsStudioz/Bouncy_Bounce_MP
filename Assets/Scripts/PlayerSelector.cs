using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerSelector : NetworkBehaviour
{
    private bool isHostTurn;

    void Start()
    {

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        NetworkManager.OnServerStarted += NetworkManager_OnServerStarted;
    }

    private void NetworkManager_OnServerStarted()
    {
        if (!IsServer)
            return;

        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.State gameState)
    {
        switch (gameState)
        {
            case GameManager.State.Game:
                Initialize();
                break;
        }
    }

    private void Initialize()
    {
        // Look for every player in the game
        PlayerStateManager[] playerStateManagers = FindObjectsOfType<PlayerStateManager>(); // find all object that contain the type "PlayerStateManager", store them in an array.

        for (int i = 0; i < playerStateManagers.Length; i++) // check i by the number of playerStateManagers (size) with a for loop.
        {
            if (playerStateManagers[i].GetComponent<NetworkObject>().IsOwnedByServer) // if this is the host.
            {
                // this is the host:
                // if it's the host's turn, enable the host
                // disable the client:
                if (isHostTurn)
                    playerStateManagers[i].Enable();
                else
                    playerStateManagers[i].Disable();
            }
            else
            {
                if (isHostTurn)
                    playerStateManagers[i].Disable();
                else
                    playerStateManagers[i].Enable();
            }
        }
    }

    void Update()
    {
        
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        NetworkManager.OnServerStarted -= NetworkManager_OnServerStarted;
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }
}

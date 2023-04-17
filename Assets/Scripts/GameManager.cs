using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;


    public enum State
    {
        Menu,
        Game,
        Win,
        Lost
    }

    private State gameState;
    //
    [SerializeField] private int connectedPlayers;

    [Header("Events")]
    public static Action<State> OnGameStateChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // When server starts, throw event
        NetworkManager.OnServerStarted += NetworkManager_OnServerStarted;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback; // unsubscribe from this event.

    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        //NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback; // unsubscribe from this event.
        NetworkManager.OnServerStarted -= NetworkManager_OnServerStarted; // When server starts, or host starts, throw event
    }

    private void NetworkManager_OnServerStarted()
    {
        if (!IsServer) // if this is NOT the host or server.
            return;

        // If it is the host or server, go here:
        connectedPlayers++; // add to the variable an additional player.
        //
        // deleted when bugs fixed.
        StartGame(); // start the game.
        //

        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback; // when client connected ,subscribe to an event
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        connectedPlayers++;

        if (connectedPlayers >= 2) // if there are 2 players
        {
            StartGame(); // start the game.
        }
    }

    private void StartGame()
    {
        StartGameClientRpc(); // make a ClientRpc call.
        Debug.Log("StartGame() passed");
    }

    [ClientRpc]
    private void StartGameClientRpc()
    {
        gameState = State.Game;
        OnGameStateChanged?.Invoke(gameState);
        Debug.Log("StartGameClientRpc() passed");
    }

    void Start()
    {
        gameState = State.Menu;
    }


    void Update()
    {
        
    }

    // backup code:
/*    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        if (connectedPlayers >= 1) // if there are 2 players
        {
            StartGame(); // start the game.
        }
    }*/


}

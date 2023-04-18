using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using TMPro;
using System.Drawing;

public class ScoreManager : NetworkBehaviour
{


    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int hostScore;
    [SerializeField] private int clientScore;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        NetworkManager.OnServerStarted += NetworkManager_OnServerStarted;
    }

    private void NetworkManager_OnServerStarted()
    {
        if (!IsServer)
            return;

        Ball.OnFellInWater += BallFellInWaterCallback;
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.State gameState)
    {
        switch (gameState)
        {
            case GameManager.State.Game:
                ResetScores();
                break;
        }
    }

    private void ResetScores()
    {
        hostScore = 0;
        clientScore = 0;

        UpdateScoreClientRpc(hostScore, clientScore);
        UpdateScoreText();
    }

    private void BallFellInWaterCallback()
    {
        if (!PlayerSelector.instance.GetIsHostTurn())
            hostScore++;
        else
            clientScore++;

        // Update text:
        UpdateScoreClientRpc(hostScore, clientScore);
        UpdateScoreText();

        CheckForEndGame();
    }

    private void CheckForEndGame()
    {
        if (hostScore >= 3)
        {
            // host wins:
            HostWin();
        }
        else if (clientScore >= 3)
        {
            // client wins:
            ClientWin();
        }
        else
        {
            // respawn ball and set its gravity on a timer:
            ReuseBall();
        }
    }

    private void ReuseBall()
    {
        BallManager.instance.ReuseBall(); // call the ReuseBall method from BallManager
    }

    private void UpdateScoreText()
    {
        UpdateScoreTextClientRpc(); // call a client Rpc and update all clients.
    }

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        
    }

    private void HostWin()
    {
        HostWinClientRpc();
    }

    [ClientRpc]
    private void HostWinClientRpc()
    {
        if (IsServer) // if the host win.
            GameManager.instance.SetGameState(GameManager.State.Win);
        else
            GameManager.instance.SetGameState(GameManager.State.Lost);
    }

    private void ClientWin()
    {
        ClientWinClientRpc();
    }

    [ClientRpc]
    private void ClientWinClientRpc()
    {
        if (IsServer) // if the host win.
            GameManager.instance.SetGameState(GameManager.State.Lost);
        else
            GameManager.instance.SetGameState(GameManager.State.Win);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        NetworkManager.OnServerStarted -= NetworkManager_OnServerStarted;
        Ball.OnFellInWater -= BallFellInWaterCallback;
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    [ClientRpc]
    private void UpdateScoreClientRpc(int hostScore, int clientScore)
    {
        // attribute these values to the local variables (hostScore + clientScore)
        this.hostScore = hostScore; 
        this.clientScore = clientScore;
    }

    [ClientRpc]
    private void UpdateScoreTextClientRpc()
    {
        scoreText.text = "<color=#0055ffff> " + hostScore + "</color> - <color=#ff5500ff> " + clientScore + "</color>";
    }
}

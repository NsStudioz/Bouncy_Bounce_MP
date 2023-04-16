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
    private int hostScore;
    private int clientScore;

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
    }

    private void BallFellInWaterCallback()
    {
        if (PlayerSelector.instance.GetIsHostTurn())
            hostScore++;
        else
            clientScore++;

        // Update text:
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "<color=#0055ffff> " + hostScore + "</color> - <color=#ff5500ff> " + clientScore + "</color>";
    }

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        NetworkManager.OnServerStarted -= NetworkManager_OnServerStarted;
        Ball.OnFellInWater -= BallFellInWaterCallback;
    }
}

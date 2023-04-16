using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    void Start()
    {
        ShowConnectionPanel();

        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.State gameState)
    {
        switch (gameState)
        {
            case GameManager.State.Game:
                ShowGamePanel();
                break;

            //case GameManager.State.
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    void Update()
    {
        
    }

    private void ShowConnectionPanel()
    {
        connectionPanel.SetActive(true);
        waitingPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    private void ShowWaitingPanel()
    {
        connectionPanel.SetActive(false);
        waitingPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    private void ShowGamePanel()
    {
        connectionPanel.SetActive(false);
        waitingPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    // may be public
    private void HostButtonCallback()
    {
        NetworkManager.Singleton.StartHost();
        ShowWaitingPanel();
    }

    private void ClientButtonCallback()
    {
        NetworkManager.Singleton.StartClient();
        ShowWaitingPanel();
    }
}

// Trash Code:
/*    private void OnEnable()
    {
        hostButton.onClick.AddListener(HostButtonCallback);
        clientButton.onClick.AddListener(ClientButtonCallback);
    }

    private void OnDisable()
    {
        hostButton.onClick.RemoveAllListeners();
        clientButton.onClick.RemoveAllListeners();
    }*/


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lostPanel;

    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    [SerializeField] private GameObject IpPanel;

    private void Awake()
    {
        SetIpPanelOnBuilds();
    }

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

            case GameManager.State.Win:
                ShowWinPanel();
                break;

            case GameManager.State.Lost:
                ShowLostPanel();
                break;
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

        winPanel.SetActive(false);
        lostPanel.SetActive(false);
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

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
    private void ShowLostPanel()
    {
        lostPanel.SetActive(true);
    }

    public void NextButtonCallback()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the active scene.

        NetworkManager.Singleton.Shutdown(); // exit host.
    }


    // may be public
    private void HostButtonCallback()
    {
        NetworkManager.Singleton.StartHost();
        ShowWaitingPanel();
    }

    private void ClientButtonCallback()
    {
#if UNITY_STANDALONE_WIN
        SetIPConnection();
#endif

        NetworkManager.Singleton.StartClient();
        ShowWaitingPanel();
    }


    private void SetIPConnection()
    {
        // grab the IP Address that the client has entered:
        string ipAddress = IPManager.instance.GetInputIP();

        // Configure the Network Manager:
        UnityTransport utp = NetworkManager.Singleton.GetComponent<UnityTransport>(); // Get the Unity Transport.
        utp.SetConnectionData(ipAddress, 7777); // Use the ip address string typed in the input field to set the connection data of this transport and connect to a host.
    }

    // Show/Hide the IP Panel based on the platform builds (Windows || Android)
    private void SetIpPanelOnBuilds()
    {
#if UNITY_STANDALONE_WIN
        IpPanel.SetActive(true); // if on windows platform, show this panel.
#else
        IpPanel.SetActive(false); // if on any other platform, hide this panel.
#endif
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


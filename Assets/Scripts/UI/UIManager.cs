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
    }

    private void OnEnable()
    {
        hostButton.onClick.AddListener(HostButtonCallback);
        clientButton.onClick.AddListener(ClientButtonCallback);
    }

    private void OnDisable()
    {
        hostButton.onClick.RemoveAllListeners();
        clientButton.onClick.RemoveAllListeners();
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

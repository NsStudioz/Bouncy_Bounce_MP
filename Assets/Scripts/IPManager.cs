using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;


public class IPManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    public static IPManager instance;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private TMP_InputField ipInputField;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ipText.text = GetLocalIPv4();

        // Get the Unity Transport:
        UnityTransport utp = NetworkManager.Singleton.GetComponent<UnityTransport>();

        // set the connection data of this transport:
        utp.SetConnectionData(GetLocalIPv4(), 7777); 
        // ipv4 = the ip address from our device. port = what we have in Unity Editor => network manager GO => Unity Transport script => Connection Data => Port.
    }

    void Update()
    {
        
    }

    public string GetInputIP()
    {
        return ipInputField.text;
    }


    // get the device IP Address from Unity:
    public string GetLocalIPv4() 
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
    }
#endif
}

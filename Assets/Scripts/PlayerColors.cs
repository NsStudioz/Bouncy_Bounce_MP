using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerColors : NetworkBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer[] renderers;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsServer && IsOwner) // if is client and owner
        {
            ColorizeServerRpc(Color.red);
        }

        ColorizeServerRpc(Color.red);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    [ServerRpc]
    private void ColorizeServerRpc(Color color)
    {
        ColorizeClientRpc(color); // calling a serverRpc to call a clientRpc so that the server will send the message across all clients.
    }

    [ClientRpc]
    private void ColorizeClientRpc(Color color)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.color = color;
        }
    }




}

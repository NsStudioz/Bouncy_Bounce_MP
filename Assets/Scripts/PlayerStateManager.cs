using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerStateManager : NetworkBehaviour
{

    [Header("Elements")]
    [SerializeField] private SpriteRenderer[] renderers;
    [SerializeField] private Collider2D col2D;

    public void Enable()
    {
        EnableClientRpc(); // send message to all clients.
    }

    public void Disable()
    {
        DisableClientRpc(); // send message to all clients.
    }

    [ClientRpc] // so that the clients can receive this code block.
    private void EnableClientRpc() // "enable renderer"
    {
        col2D.enabled = true; 

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }

    [ClientRpc] // so that the clients can receive this code block.
    private void DisableClientRpc() // "disable renderer"
    {
        col2D.enabled = false;

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = 0.2f;
            renderer.color = color;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

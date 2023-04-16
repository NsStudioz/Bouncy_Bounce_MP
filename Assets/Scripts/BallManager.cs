using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class BallManager : NetworkBehaviour
{
    public static BallManager instance;

    [Header("Elements")]
    [SerializeField] private Ball ballPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }



    void Update()
    {
        
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.State gameState)
    {
        switch (gameState)
        {
            case GameManager.State.Game:
                SpawnBall();
                break;

                //case GameManager.State.
        }
    }

    private void SpawnBall()
    {
        if (!IsServer)
            return;

        //Ball ballInstance = Instantiate(ballPrefab, Vector2.up * 5, Quaternion.identity, transform); // apply the parent Ball Manager to Ball prefab. So ball is child of Ball Manager.
        Ball ballInstance = Instantiate(ballPrefab, Vector2.up * 5, Quaternion.identity); // spawn object locally. must spawn it locally first. Only host can see it here.
        ballInstance.GetComponent<NetworkObject>().Spawn(); // spawn object on the network. now both host and clients can see it.

        ballInstance.transform.SetParent(transform); // set the ball instance as child of this game object.
    }

    public void ReuseBall()
    {
        if (!IsServer)
            return;

        if (transform.childCount <= 0) // if there is no ball, return. this method will be called if there is at least 1 ball.
            return;

        transform.GetChild(0).GetComponent<Ball>().Reuse();
    }

}

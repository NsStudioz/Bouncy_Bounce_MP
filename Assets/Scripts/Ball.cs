using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // add the rigidbody component automatically on instantiation.
public class Ball : MonoBehaviour
{
    [Header("Physics Settings")]
    Rigidbody2D rigidBody2D;
    //
    [SerializeField] private float bounceVelocity;
    [SerializeField] private bool isAlive;

    [Header("Events")]
    public static Action OnHit;
    public static Action OnFellInWater;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        isAlive = true;
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!isAlive)
            return;

        if (col.collider.TryGetComponent(out PlayerController playerController)) // if the collider has the component type of PlayerController...
        {
            Bounce(col.GetContact(0).normal);
            OnHit?.Invoke();
        }
    }

    private void Bounce(Vector2 normal)
    {
        rigidBody2D.velocity = normal * bounceVelocity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Water"))
        {
            if (!isAlive)
                return;

            OnFellInWater?.Invoke();
            isAlive = false;
        }
    }
}

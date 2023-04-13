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


    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.TryGetComponent(out PlayerController playerController)) // if the collider has the component type of PlayerController...
        {
            Bounce(col.GetContact(0).normal);
        }
    }

    private void Bounce(Vector2 normal)
    {
        rigidBody2D.velocity = normal * bounceVelocity;
    }
}

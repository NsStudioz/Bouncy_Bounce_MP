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
    [SerializeField] private float gravityScale;

    [Header("Events")]
    public static Action OnHit;
    public static Action OnFellInWater;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        isAlive = true;

        gravityScale = rigidBody2D.gravityScale; // initialize gravityScale at start frame.
        rigidBody2D.gravityScale = 0; // set rigidbody gravity off. regular gravityScale variable does not change here.

        // wait 2 seconds, then fall:
        StartCoroutine("WaitAndFall");
    }

    IEnumerator WaitAndFall()
    {
        yield return new WaitForSeconds(2);

        rigidBody2D.gravityScale = gravityScale; // set the rigidbody gravity on.
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
            //
            OnHit?.Invoke();
        }
    }

    private void Bounce(Vector2 normal)
    {
        rigidBody2D.velocity = normal * bounceVelocity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isAlive)
            return;

        if (col.CompareTag("Water"))
        {
            isAlive = false;    // this must start first, to prevent execution order issues when an event starts.
            OnFellInWater?.Invoke();
        }
    }

    public void Reuse()
    {
        // set position, set gravity and velocities off:
        transform.position = Vector2.up * 5;
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0; // disable rotation
        rigidBody2D.gravityScale = 0;

        // set triggers on for ball:
        isAlive = true;

        // set timer and gravity on
        StartCoroutine("WaitAndFall");
    }

}

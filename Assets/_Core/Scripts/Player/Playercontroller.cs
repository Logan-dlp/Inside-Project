using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Playercontroller : MonoBehaviour
{
    private Rigidbody rb;

    public float Speed = 5;
    public float JumpForce = 5;
    
    public KeyCode Avancer = KeyCode.Z;
    public KeyCode Reculer = KeyCode.S;
    public KeyCode Droite = KeyCode.D;
    public KeyCode Gauche = KeyCode.Q;
    public KeyCode Jump = KeyCode.Space;

    private bool IsJumping = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        Movements();
        Jumps();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            IsJumping = false;
        }
    }

    void Movements()
    {
        if (Input.GetKey(Avancer))
        {
            rb.AddForce(transform.forward * Speed);
        }else if (Input.GetKey(Reculer))
        {
            rb.AddForce(-transform.forward * Speed);
        } else if (Input.GetKey(Droite))
        {
            rb.AddForce(transform.right * Speed);
        }else if (Input.GetKey(Gauche))
        {
            rb.AddForce(-transform.right * Speed);
        }
    }

    void Jumps()
    {
        if (!IsJumping)
        {
            if (Input.GetKeyDown(Jump))
            {
                rb.AddForce(transform.up * JumpForce);
                IsJumping = true;
            }
        }
    }
}

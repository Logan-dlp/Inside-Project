using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour, IInterctable
{
    private Animator animator;
    private bool on = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_Power", 100);
    }

    public void Interact(GameObject _player)
    {
        on = !on;
        animator.SetBool("On", on);
        _player.gameObject.GetComponent<PlayerController>().ApplyLight(on);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour, IInterctable
{
    private Animator animator;
    private bool animationOn = true;

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
        animator.SetBool("On", animationOn);
        animationOn = !animationOn;
        _player.gameObject.GetComponent<PlayerController>().ApplyLight(!_player.gameObject.GetComponent<PlayerController>().LightIsActive);
    }
}

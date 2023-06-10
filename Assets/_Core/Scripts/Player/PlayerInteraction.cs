using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float RayDistance = 2;
    public float RayHeight = 1.3f;
    private PlayerInput playerInput;
    private Animator animator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        InputAction _interact = playerInput.actions["Interaction"];
        _interact.performed += InteractPerformed;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, RayHeight, 0), transform.forward * RayDistance, color:Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0, RayHeight, 0), transform.forward, out RaycastHit _hit, RayDistance))
        {
            if (_hit.collider.gameObject.TryGetComponent(out IInterctable _interctable))
            {
                _hit.collider.gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_Power", 2.5f);
            }
        }
    }

    void InteractPerformed(InputAction.CallbackContext _ctx)
    {
        
        if (Physics.Raycast(transform.position + new Vector3(0, RayHeight, 0), transform.forward, out RaycastHit _hit, RayDistance))
        {
            if (_hit.collider.gameObject.TryGetComponent(out IInterctable _interctable))
            {
                animator.SetTrigger("Interact");
                _interctable.Interact(gameObject);
            }
            else
            {
                animator.SetTrigger("NoInteract");
            }
        }
        else
        {
            animator.SetTrigger("NoInteract");
        }

    }
}

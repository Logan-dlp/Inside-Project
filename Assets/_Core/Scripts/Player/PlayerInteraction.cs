using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator animator;
    private Vector3 rayHeight;
    
    [Header("RayCast settings")]
    public float RayDistance = 2;
    public float RayHeight = 1.3f;
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        InputAction _interact = playerInput.actions["Interaction"];
        _interact.performed += InteractPerformed;

        rayHeight = new Vector3(0, RayHeight, 0);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + rayHeight, transform.forward * RayDistance, color:Color.red);
        if (Physics.Raycast(transform.position + rayHeight, transform.forward, out RaycastHit _hit, RayDistance))
        {
            if (_hit.collider.gameObject.TryGetComponent(out IInterctable _interctable))
            {
                _hit.collider.gameObject.GetComponentInChildren<Renderer>().material.SetFloat("_Power", 2.5f);
            }
        }
    }

    void InteractPerformed(InputAction.CallbackContext _ctx)
    {
        if (Physics.Raycast(transform.position + rayHeight, transform.forward, out RaycastHit _hit, RayDistance))
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

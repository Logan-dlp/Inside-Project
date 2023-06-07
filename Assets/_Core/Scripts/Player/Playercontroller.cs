using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject light;

    [Header("Physiques du joueur")]
    public float Gravity;
    public float Speed;

    [HideInInspector] public bool LightIsActive;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        light = GameObject.FindWithTag("PlayerLight");
        ApplyLight(false);
    }

    private void Update()
    {
        ApplyGravity();
        Mouvement();
    }

    void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            Vector3 _gravityDirection = Vector3.zero;
            _gravityDirection.y = -Gravity;
            controller.Move(_gravityDirection * Time.deltaTime);
        }
    }

    void Mouvement()
    {
        Vector3 _playerDirection = Vector3.zero;
        _playerDirection.x += Input.GetAxis("Horizontal") * Time.deltaTime;
        _playerDirection.z += Input.GetAxis("Vertical") * Time.deltaTime;
        controller.Move(_playerDirection * Speed);
    }

    public void ApplyLight(bool _on)
    {
        light.SetActive(_on);
        LightIsActive = _on;
    }
}

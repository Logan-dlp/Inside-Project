using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ButtonInteract : MonoBehaviour
{
    private bool ActiveButton = false;

    public Material Glow;

    private void Start()
    {
        Glow.SetFloat("_Intensity", 1);
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!ActiveButton)
            {
                Glow.SetFloat("_Intensity", 100);
                PlayerController _player = _collider.GetComponent<PlayerController>();
                _player.ApplyLight(!_player.LightIsActive);
                StartCoroutine("WaitActiveButton");
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Glow.SetFloat("_Intensity", 1);
        }
    }

    IEnumerator WaitActiveButton()
    {
        ActiveButton = true;
        yield return new WaitForSeconds(1);
        ActiveButton = false;
    }
}

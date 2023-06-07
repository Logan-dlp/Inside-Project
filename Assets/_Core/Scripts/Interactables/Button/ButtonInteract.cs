using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ButtonInteract : MonoBehaviour
{
    private bool ActiveButton = false;
    
    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!ActiveButton)
            {
                PlayerController _player = _collider.GetComponent<PlayerController>();
                _player.ApplyLight(!_player.LightIsActive);
                StartCoroutine("WaitActiveButton");
                return;
            }
        }
    }

    IEnumerator WaitActiveButton()
    {
        ActiveButton = true;
        yield return new WaitForSeconds(1);
        ActiveButton = false;
    }
}

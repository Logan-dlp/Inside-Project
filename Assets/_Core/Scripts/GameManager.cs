using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Player;
    public Vector2 Resolution = new Vector2(1920, 1080);
    public bool UiOn = false;
    
    Vector3 cursorPosition = Vector3.zero;

    private void Update()
    {
        ApplyResolution();

        if (UiOn)
        {
            if (cursorPosition != Input.mousePosition)
            {
                ApplyCursor(true, false);
                StartCoroutine(CursorMove());
            }
            else
            {
                ApplyCursor(false, false);
            }
        }
        else
        {
            ApplyCursor(false, true);
        }
    }

    public void ApplyCursor(bool _on, bool _lock)
    {
        if (!_lock) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = _on;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution((int)Resolution.x, (int)Resolution.y, true);
    }

    IEnumerator CursorMove()
    {
        yield return new WaitForSeconds(5);
        cursorPosition = Input.mousePosition;
    }
}

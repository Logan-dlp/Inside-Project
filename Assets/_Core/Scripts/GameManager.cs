using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Player;
    public Vector2 Resolution = new Vector2(1920, 1080);

    private void Update()
    {
        ApplyResolution();
        ApplyCursor(false);
    }

    public void ApplyCursor(bool _on)
    {
        if (_on) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = _on;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution((int)Resolution.x, (int)Resolution.y, true);
    }
}

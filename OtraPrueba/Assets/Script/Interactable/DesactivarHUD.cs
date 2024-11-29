using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarHUD : MonoBehaviour
{
    public GameObject Icon;
    public void HUD ()
    {
        Icon.SetActive (true);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

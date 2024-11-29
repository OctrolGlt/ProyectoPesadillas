using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableNote : Interactable
{
    public GameObject nota;
    public GameObject Icono;

    public override void Interact()
    {
        base.Interact();
        Icono.SetActive(false);
        Time.timeScale = 0;
        nota.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
}

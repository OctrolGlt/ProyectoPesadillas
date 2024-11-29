using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomarLinterna : Interactable
{
    public GameObject objetoImg;
    public GameObject Icono;
    public GameObject Linterna;

    public override void Interact()
    {
        base.Interact();

        Linterna.SetActive(true);
        Icono.SetActive(false);
        Time.timeScale = 0;
        objetoImg.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.enabled = false;


    }
}

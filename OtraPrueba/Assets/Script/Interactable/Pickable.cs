using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    public GameObject objetoImg;
    public GameObject Icono;
    
    public override void Interact()
    {
        base.Interact();

        gameObject.SetActive(false);
        Icono.SetActive(false);
        Time.timeScale = 0;
        objetoImg.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        
    }
}

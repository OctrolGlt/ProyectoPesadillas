using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fusibles : Interactable
{
    public Inventario objeto;
    
    public override void Interact()
    {
        base.Interact();
        gameObject.SetActive(false);

        objeto.AumentoDeFusibles();

    }
}
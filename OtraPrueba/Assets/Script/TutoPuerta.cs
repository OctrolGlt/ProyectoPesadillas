using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPuerta : MonoBehaviour
{
    public AudioClip pasos;
    private bool sonidoReproducido = false; // Variable para controlar si el sonido ya ha sido reproducido

    private void OnTriggerEnter(Collider other)
    {
        // Si el sonido no ha sido reproducido aún
        if (!sonidoReproducido)
        {
            Debug.Log("Abrir");
            AudioSource.PlayClipAtPoint(pasos, transform.position, 1);
            sonidoReproducido = true; // Marcar que ya se ha reproducido
        }
    }
}

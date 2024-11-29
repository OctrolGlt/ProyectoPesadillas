using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : Interactable
{
    public AudioClip generador; // Audio que se reproduce al interactuar
    public GameObject[] Luces; // Array de luces a activar

    public override void Interact()
    {
        // Reproduce el sonido del generador
        AudioSource.PlayClipAtPoint(generador, transform.position, 1);

        // Activa cada luz en el array
        foreach (GameObject luz in Luces)
        {
            if (luz != null) // Verifica que la luz no sea nula
            {
                luz.SetActive(true); // Activa la luz
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;
    public float doorCloseAngle = 0.0f;
    public float smoot = 3.0f;

    public GameObject PointSound;

    public AudioClip openDoor;
    public AudioClip closeDoor;

    private bool canPlaySound = false; // Variable para controlar si se puede reproducir el sonido

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Puerta");
        doorOpen = !doorOpen;
        canPlaySound = true; // Habilita la reproducción del sonido después de la primera interacción
    }

    private void Update()
    {
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerDoor" && canPlaySound) // Verifica si se puede reproducir el sonido
        {
            Debug.Log("Sonido de cerrar puerta");
            AudioSource.PlayClipAtPoint(closeDoor, PointSound.transform.position, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TriggerDoor" && canPlaySound) // Verifica si se puede reproducir el sonido
        {
            Debug.Log("Sonido de abrir puerta");
            AudioSource.PlayClipAtPoint(openDoor, PointSound.transform.position, 1);
        }
    }


}

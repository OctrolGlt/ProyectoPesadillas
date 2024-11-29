using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Interactable
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;
    public float doorCloseAngle = 0.0f;
    public float smoot = 3.0f;

    public GameObject key;

    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioClip LockDoor;
    //public AudioClip ghost;

    private bool canPlaySound = false;


    public override void Interact()
    {
        base.Interact();
        //Debug.Log("Puerta");
        if (!key.activeSelf)
        {
            doorOpen = !doorOpen;
            canPlaySound = true;
        }
        else
        {
            Debug.Log("Bloqueada");
            AudioSource.PlayClipAtPoint(LockDoor, transform.position, 1);
            //AudioSource.PlayClipAtPoint(ghost, transform.position, 1);
        }
        
    }

    private void Update()
    {

        if (!key.activeSelf)
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
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerDoor" && canPlaySound) // Verifica si se puede reproducir el sonido
        {
            Debug.Log("Sonido de cerrar puerta");
            AudioSource.PlayClipAtPoint(closeDoor, transform.position, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TriggerDoor" && canPlaySound) // Verifica si se puede reproducir el sonido
        {
            Debug.Log("Sonido de abrir puerta");
            AudioSource.PlayClipAtPoint(openDoor, transform.position, 1);
        }
    }
}

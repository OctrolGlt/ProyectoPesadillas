using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : Interactable
{
    public AudioClip LockDoor;
    public AudioClip PuertaAzote;
    public GameObject PointSound;
    public GameObject trigger;
    public override void Interact()
    {

        base.Interact();
        //Debug.Log("Puerta");
        
        Debug.Log("Bloqueada");
        AudioSource.PlayClipAtPoint(LockDoor, PointSound.transform.position, 1);
        if (trigger != null)
        {
            trigger.SetActive(true);
        }
        //AudioSource.PlayClipAtPoint(ghost, transform.position, 1);


    }

    public void SonidosdeGolpe()
    {
        AudioSource.PlayClipAtPoint(PuertaAzote, PointSound.transform.position, 1);
    }
}

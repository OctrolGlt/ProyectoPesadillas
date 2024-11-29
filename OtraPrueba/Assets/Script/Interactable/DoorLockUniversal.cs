using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockUniversal : Interactable
{
    public AudioClip LockDoor;
    //public AudioClip PuertaAzote;
    public GameObject PointSound;
    public override void Interact()
    {

        base.Interact();
        //Debug.Log("Puerta");

        Debug.Log("Bloqueada");
        AudioSource.PlayClipAtPoint(LockDoor, PointSound.transform.position, 1);
        //AudioSource.PlayClipAtPoint(ghost, transform.position, 1);


    }

    
}

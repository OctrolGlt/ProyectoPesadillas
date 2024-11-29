using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public DoorLock objeto;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objeto.SonidosdeGolpe();
            Destroy(gameObject);
        }
        
    }
}

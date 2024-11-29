using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encendido_Apagado : MonoBehaviour
{

    public GameObject luzObjeto;

    public bool luz;

    public bool luzOnOff;
    
    public void OnOffLuz()
    {
        if (luzOnOff == true)
        {
            luzObjeto.SetActive(true);
        }

        if (luzOnOff == false)
        {
            luzObjeto.SetActive(false);
        }
    }
}

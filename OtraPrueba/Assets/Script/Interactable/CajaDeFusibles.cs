using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CajaDeFusibles : Interactable
{
    public Inventario inv;
    public int n, x;
    public GameObject Puerta;
    public AudioClip sonidoPuerta;

    public GameObject cambioNivel;

    public float velocidad = 2.5f; // Velocidad del movimiento
    public float incremento = 1f; // Distancia que se mueve en cada paso
    public float tiempoEntreMovimientos = 0.1f; // Tiempo entre cada movimiento


    public GameObject mensajeTexto;
    public float tiempoMostrarTexto = 5f;
    public override void Interact()
    {
        base.Interact();

        n = inv.Numerof();

        if (n == 5)
        {
            AudioSource.PlayClipAtPoint(sonidoPuerta, Puerta.transform.position, 1);
            StartCoroutine(MoverHaciaArriba());
            Debug.Log("Abriendo");
            cambioNivel.SetActive(true);
        }
        else
        {
            Debug.Log("Faltan");
            StartCoroutine(MostrarTextoPorTiempo());
        }
        
    }

    private IEnumerator MoverHaciaArriba()
    {
        while (x<22) // Bucle infinito para mover constantemente
        {
            // Mueve el objeto hacia arriba
            Puerta.transform.position += Vector3.up * incremento;

            // Espera antes de realizar el siguiente movimiento
            yield return new WaitForSeconds(tiempoEntreMovimientos);
            x++;
        }
    }

    private IEnumerator MostrarTextoPorTiempo()
    {
        mensajeTexto.SetActive(true); // Activa el texto
        yield return new WaitForSeconds(tiempoMostrarTexto); // Espera el tiempo especificado
        mensajeTexto.SetActive(false); // Desactiva el texto
    }
}

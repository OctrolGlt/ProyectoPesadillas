using System.Collections;
using UnityEngine;
using TMPro; // Importar TextMeshPro

public class TextoInicial : MonoBehaviour
{
    public string[] mensajes; // Array de mensajes
    public TMP_Text mensajeTexto; // Referencia al objeto de texto de TextMeshPro
    public float tiempoMostrarTexto = 5f; // Tiempo que se mostrará cada mensaje
    private int pos = 0; // Índice del mensaje actual
    public float retrasoInicial = 5f;

    void Start()
    {
        StartCoroutine(MostrarMensajes());
    }

    private IEnumerator MostrarMensajes()
    {

        yield return new WaitForSeconds(retrasoInicial);
        while (pos < mensajes.Length)
        {
            mensajeTexto.text = mensajes[pos]; // Actualiza el texto con el mensaje actual
            mensajeTexto.gameObject.SetActive(true); // Asegúrate de que el texto esté activo
            yield return new WaitForSeconds(tiempoMostrarTexto); // Espera el tiempo especificado
            mensajeTexto.gameObject.SetActive(false); // Oculta el texto
            pos++; // Avanza al siguiente mensaje
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CambioNivel : Interactable
{
    public int Nivel;
    public Image barra;

    AsyncOperation Operacion;

    public override void Interact()
    {

        base.Interact();
    
        
            StartCoroutine(Cargando(Nivel));
        
    }

    IEnumerator Cargando(int Nivel)
    {
        Operacion = SceneManager.LoadSceneAsync(Nivel);

        while (!Operacion.isDone)
        {
            barra.fillAmount = Operacion.progress;
            yield return null;
        }
    }
}

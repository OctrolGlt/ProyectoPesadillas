using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public int Nivel;
    public Image barra;

    AsyncOperation Operacion;

    public void Nivel1()
    {
        Nivel = 1;
        StartCoroutine(Cargando(Nivel));
    }

    public void Nivel2()
    {
        Nivel = 2;
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

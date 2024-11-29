using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Image barra;

    AsyncOperation Operacion;
    public void EmpezarNivel(string NombreNivel)
    {
        StartCoroutine(Cargando(NombreNivel));
        //SceneManager.LoadScene(NombreNivel);
    }

    IEnumerator Cargando(string NombreNivel)
    {
        Operacion = SceneManager.LoadSceneAsync(NombreNivel);

        while (!Operacion.isDone)
        {
            barra.fillAmount = Operacion.progress;
            yield return null;
        }
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo");
    }
}

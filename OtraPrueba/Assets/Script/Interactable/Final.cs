using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HDROutputUtils;

public class Final : Interactable
{

    public GameObject canvasFinal;
    public GameObject menuPausa;
    public AudioSource songEscenario;
    public int Nivel;
    AsyncOperation Operacion;

    public Image barra;
    public override void Interact()
    {
        base.Interact();
        songEscenario.Stop();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuPausa.SetActive(false);
        canvasFinal.SetActive(true);

    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1f;
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

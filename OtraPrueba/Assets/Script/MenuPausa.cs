using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HDROutputUtils;

public class MenuPausa : MonoBehaviour
{
    public int Nivel;
    public Image barra;

    AsyncOperation Operacion;

    public GameObject menuPausa;
    public bool pausa = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa == false)
            {
                menuPausa.SetActive(true);
                pausa = true;

                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                menuPausa.SetActive(false);
                pausa = false;

                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
    }

    public void Resumir()
    {
        menuPausa.SetActive(false);
        pausa = false;

        Debug.Log("Resumir");

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo");
    }

    
}

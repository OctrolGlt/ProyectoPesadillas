using UnityEngine;

public class Moho : MonoBehaviour
{
    public float velocidadDesvanecimiento = 1.0f; 
    private Material[] materiales; 
    private Color[] coloresIniciales; 
    public GameObject foco;

    private bool desvaneciendo = false;
    private float alpha = 1.0f; 

    void Start()
    {
        Time.timeScale = 1;
        // Obtener todos los materiales del objeto y sus hijos
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        materiales = new Material[renderers.Length];
        coloresIniciales = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materiales[i] = renderers[i].material; 
            coloresIniciales[i] = materiales[i].color; 
        }
    }

    void Update()
    {
        // Iniciar el desvanecimiento cuando sea necesario
        if (foco.activeSelf)
        {
            Debug.Log("Desvaneciendo");
            alpha -= velocidadDesvanecimiento * Time.deltaTime; 
            alpha = Mathf.Clamp01(alpha); 

            
            for (int i = 0; i < materiales.Length; i++)
            {
                Color newColor = new Color(coloresIniciales[i].r, coloresIniciales[i].g, coloresIniciales[i].b, alpha);
                materiales[i].color = newColor; 
            }

            // Detener el desvanecimiento si alpha llega a 0
            if (alpha <= 0)
            {
                //desvaneciendo = false;
                gameObject.SetActive(false);
            }
        }
    }

    // Llamar a este método para iniciar el desvanecimiento
    public void IniciarDesvanecimiento()
    {
        desvaneciendo = true;
    }
}

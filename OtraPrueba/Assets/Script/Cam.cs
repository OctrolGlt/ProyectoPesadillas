using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform _camera;
    public float camearaSensitivity = 200f;
    public float cameraAcceleration = 5.0f;

    public Transform hand;
    public GameObject interactableImage;
    public GameObject interactableText;

    private float rotation_x_axis;
    private float rotation_y_axis;

    public float distancia = 1.5f;

    public Light luz;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        interactableImage.SetActive(false); // Asegúrate de que la imagen esté desactivada al inicio
        interactableText.SetActive(false);
    }

    void Update()
    {
        Debug.DrawRay(_camera.position, _camera.forward * distancia, Color.red);

        // Realiza el Raycast continuamente para detectar colisiones
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, distancia, LayerMask.GetMask("Interactable")))
        {
            // Si el Raycast colisiona con un objeto interactuable, activa la imagen
            interactableImage.SetActive(true);
            interactableText.SetActive(true);

            // También puedes agregar la interacción cuando presiones la tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                hit.transform.GetComponent<Interactable>().Interact();
                
            }
        }
        else
        {
            // Si no está colisionando con un objeto interactuable, desactiva la imagen
            interactableImage.SetActive(false);
            interactableText.SetActive(false);
        }

        // Activar o desactivar la luz con la tecla F solo si el GameObject está activo
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.F) && luz != null)
        {
            luz.enabled = !luz.enabled; // Cambia el estado de la luz
        }

        // Rotación de la cámara y la mano (control del movimiento)
        rotation_x_axis += Input.GetAxis("Mouse Y") * camearaSensitivity * Time.deltaTime;
        rotation_y_axis += Input.GetAxis("Mouse X") * camearaSensitivity * Time.deltaTime;

        rotation_x_axis = Mathf.Clamp(rotation_x_axis, -60.0f, 60.0f);

        hand.localRotation = Quaternion.Euler(-rotation_x_axis, rotation_y_axis, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(0, rotation_y_axis, 0), cameraAcceleration * Time.deltaTime);

        _camera.localRotation = Quaternion.Lerp(_camera.localRotation,
            Quaternion.Euler(-rotation_x_axis, 0, 0), cameraAcceleration * Time.deltaTime);
    }
}


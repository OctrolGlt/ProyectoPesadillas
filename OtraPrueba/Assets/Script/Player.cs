using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform head;
    public float playerSpeed = 5.0f;
    public float playerAcceleration = 2.0f;
    public float jumpForce = 6.0f;
    public LayerMask groundLayer;
    public AudioSource walkAudioSource; // Fuente de audio para el sonido de caminar

    private CharacterController characterController;
    private Vector3 direction;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isWalking; // Para controlar si está caminando

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        isWalking = false; // Inicialmente no está caminando
    }

    private void Update()
    {
        // Verifica si el personaje está en el suelo
        isGrounded = characterController.isGrounded;

        // Obtiene la dirección de movimiento en base a la entrada del jugador
        direction = Input.GetAxisRaw("Horizontal") * head.right + Input.GetAxisRaw("Vertical") * head.forward;
        direction.Normalize(); // Normaliza la dirección

        // Mueve al jugador
        Vector3 move = direction * playerSpeed;

        if (isGrounded)
        {
            // Resetea la velocidad vertical si está en el suelo
            velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                // Aplica la fuerza de salto
                velocity.y += jumpForce;
            }
        }

        // Aplica la gravedad
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Desplaza al personaje
        characterController.Move((move + velocity) * Time.deltaTime);

        // Verifica si el jugador se está moviendo
        if (move.magnitude > 0 && isGrounded)
        {
            if (!isWalking)
            {
                walkAudioSource.Play(); // Reproduce el sonido de caminar
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                walkAudioSource.Stop(); // Detiene el sonido si deja de moverse
                isWalking = false;
            }
        }
    }
}

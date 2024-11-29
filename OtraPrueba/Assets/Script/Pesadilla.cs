using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pesadilla : MonoBehaviour
{
    public float wanderRadius = 10f; // Radio de movimiento aleatorio
    public float waitTime = 2f;      // Tiempo de espera entre destinos
    public float chaseDistance = 15f; // Distancia para detectar al jugador
    public float attackDistance = 2f; // Distancia para pausar el juego
    public LayerMask playerLayer;    // Capa del jugador
    public Transform player;         // Referencia al jugador
    public float obstacleDetectionRange = 2f; // Rango para evitar obst�culos
    public LayerMask obstacleLayer;  // Capa de obst�culos
    public Light spotLight;          // Spotlight que afecta al zombie
    public AudioSource SonidoNivel;

    //public Transform player;
    public Canvas alertCanvas; // Canvas que se activar�
    public AudioSource alertAudio; // Audio que se reproducir�
    public Image alertImage;

    public GameObject pausa;

   // private NavMeshAgent agent;
    private bool eventTriggered = false;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3 lastKnownPosition; // �ltima posici�n conocida del jugador
    private bool isChasing;           // Indica si est� persiguiendo al jugador
    private float waitTimer;          // Temporizador para esperar entre destinos
    private bool isIlluminated;       // Indica si el zombie est� siendo iluminado

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waitTimer = waitTime;
        isChasing = false;
        isIlluminated = false;
    }

    void Update()
    {
        // Verificar si el zombie est� iluminado
        CheckDirectLight();

        if (isIlluminated)
        {
            navMeshAgent.isStopped = true; // Detener el movimiento del zombie
            UpdateAnimatorSpeed(0f);       // Actualizar animaci�n a "quieto"
            return;
        }
        else
        {
            navMeshAgent.isStopped = false; // Reactivar el movimiento
        }

        // Verificar distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            PauseGame();
            return;
        }

        if (distanceToPlayer <= chaseDistance)
        {
            StartChasing();
        }
        else if (isChasing)
        {
            StopChasing();
        }

        // Comportamiento seg�n el estado
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }

        // Actualizar animaci�n seg�n la velocidad
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        UpdateAnimatorSpeed(speed);

    }

    void UpdateAnimatorSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    // Pausar el juego al alcanzar al jugador
    void PauseGame()
    {
        StartCoroutine(TriggerEvent());
        //Debug.Log("�El zombie alcanz� al jugador! Juego pausado.");
    }

    IEnumerator TriggerEvent()
    {
        SonidoNivel.enabled = false;
        pausa.SetActive(false);
        eventTriggered = true;
        navMeshAgent.isStopped = true;
        Time.timeScale = 0; // Pausa el tiempo
        alertCanvas.gameObject.SetActive(true); // Muestra el Canvas
        alertAudio.Play(); // Reproduce el sonido

        yield return new WaitForSecondsRealtime(5); // Espera 5 segundos en tiempo real

        if (alertImage != null)
        {
            alertImage.gameObject.SetActive(false); // Desactiva la imagen
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //alertCanvas.gameObject.SetActive(false); // Oculta el Canvas
        //Time.timeScale = 1; // Restaura el tiempo
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia el nivel
    }

    // Activar el modo de persecuci�n
    void StartChasing()
    {
        isChasing = true;
        lastKnownPosition = player.position;
    }

    // Detener el modo de persecuci�n y volver al movimiento aleatorio
    void StopChasing()
    {
        isChasing = false;
        SetRandomDestination();
    }

    // Perseguir al jugador
    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
        lastKnownPosition = player.position;
    }

    // Movimiento aleatorio
    void Wander()
    {
        // Detectar si lleg� al destino
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                SetRandomDestination();
                waitTimer = waitTime;
            }
        }

        // Evitar obst�culos
        if (DetectObstacle())
        {
            AvoidObstacle();
        }
    }

    // Establecer un destino aleatorio
    // Establecer un destino aleatorio
    void SetRandomDestination()
    {
        int maxAttempts = 10; // M�ximo n�mero de intentos para encontrar un destino v�lido
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius; // Generar un punto aleatorio
            randomDirection += transform.position;

            // Validar si el punto generado est� dentro del NavMesh
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                navMeshAgent.SetDestination(hit.position); // Asignar el destino si es v�lido
                return;
            }

            attempts++;
        }

        // Si no encuentra un destino despu�s de varios intentos, usar el �ltimo destino conocido
        Debug.LogWarning("No se encontr� un destino v�lido en el NavMesh. Reutilizando la posici�n actual.");
        navMeshAgent.SetDestination(transform.position);
    }

    // Detectar si hay un obst�culo enfrente
    bool DetectObstacle()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, obstacleDetectionRange, obstacleLayer);
    }

    // Evitar obst�culos redirigiendo el movimiento
    void AvoidObstacle()
    {
        Vector3 avoidDirection = Quaternion.Euler(0, Random.Range(-90, 90), 0) * transform.forward;
        Vector3 newTarget = transform.position + avoidDirection * wanderRadius;

        if (NavMesh.SamplePosition(newTarget, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }

    // Verificar si el zombie est� siendo iluminado por el Spotlight
    void CheckDirectLight()
    {
        Vector3 directionToEnemy = transform.position - spotLight.transform.position;
        float angleToEnemy = Vector3.Angle(spotLight.transform.forward, directionToEnemy);

        if (angleToEnemy <= spotLight.spotAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(spotLight.transform.position, directionToEnemy.normalized, out hit, chaseDistance))
            {
                if (hit.transform == transform)
                {
                    isIlluminated = true;
                    return;
                }
            }
        }

        isIlluminated = false;
    }
}

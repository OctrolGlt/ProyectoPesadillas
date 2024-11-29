using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float detectionRadius = 10f;
    public float stoppingDistance = 1f;
    public float patrolWaitTime = 2f;
    public Light spotLight;
    public Animator animator;
    public Canvas alertCanvas; // Canvas que se activará
    public AudioSource alertAudio; // Audio que se reproducirá
    public Image alertImage;

    public GameObject pausa;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool playerDetected = false;
    private bool isIlluminated = false;
    private float waitTimer = 0f;
    private bool eventTriggered = false; // Para evitar que el evento se repita

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        alertCanvas.gameObject.SetActive(false); // Asegúrate de que el Canvas esté desactivado al inicio
    }

    void Update()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", speed);

        CheckDirectLight();

        if (isIlluminated)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        CheckPlayerDetection();

        if (playerDetected)
        {
            ChasePlayer();

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= stoppingDistance && !eventTriggered)
            {
                StartCoroutine(TriggerEvent());
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= patrolWaitTime)
            {
                SetNextPatrolPoint();
                waitTimer = 0f;
            }
        }
    }

    void SetNextPatrolPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        Vector3 patrolPosition = patrolPoints[currentPatrolIndex].position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(patrolPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.destination = hit.position;
        }
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            agent.destination = player.position;
        }
    }

    void CheckPlayerDetection()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRadius))
            {
                playerDetected = hit.transform == player;
                return;
            }
        }

        playerDetected = false;
    }

    void CheckDirectLight()
    {
        Vector3 directionToEnemy = transform.position - spotLight.transform.position;
        float angleToEnemy = Vector3.Angle(spotLight.transform.forward, directionToEnemy);

        if (angleToEnemy <= spotLight.spotAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(spotLight.transform.position, directionToEnemy.normalized, out hit, detectionRadius))
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

    IEnumerator TriggerEvent()
    {
        pausa.SetActive(false);
        eventTriggered = true;
        agent.isStopped = true;
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
}

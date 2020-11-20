using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float velocidade = 10f;

    public Animator animator;

    Transform target;
    NavMeshAgent agent;

    GameManager GameManager;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        agent = GetComponent<NavMeshAgent>();

        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Parado", false);
            animator.SetBool("Correndo", true);

            if (distance <= agent.stoppingDistance)
            {
                // Atacar o target
                // Olhar para o target
                FaceTarget();
            }
        }
        else
        {
            animator.SetBool("Parado", true);
            animator.SetBool("Correndo", false);
        }
    }

    void FaceTarget()
    {
        //Pegando a direção para o target
        Vector3 direction = (target.position - transform.position).normalized;

        //Pegar a rotação em que estamos para apontar para o target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Atualizar a nossa rotação para aquele ponto que queremos (em direção ao target) + Fazer isso suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velocidade);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Desmatamento")
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Desmatamento")
        {
            Destroy(gameObject);
            GameManager.SetLumberjackQuantityNumber();
            //play animation "Virando fumaça"

        }
    }

    private void OnDestroy() 
    {
        GameManager.SetNPCDestroyed();
    }
}

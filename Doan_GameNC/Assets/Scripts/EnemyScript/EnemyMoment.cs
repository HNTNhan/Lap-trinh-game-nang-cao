using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoment : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AggroDetection aggroDetection;
    private Transform target;
    private Vector3 transform_child;
    private GameObject[] allEnemy;
    private int check;
    private void Awake()
    {
        check = 0;
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        transform_child = transform.GetChild(0).GetComponent<Transform>().position;
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;  
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if(target != null)
        {
            if (PlayerPrefs.GetString("Load1") == "true")
            {
                PlayerPrefs.SetString("Load1", "false");
                return;
            }
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && !animator.GetBool("Die"))
            {
                navMeshAgent.SetDestination(target.position);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                navMeshAgent.SetDestination(transform.position);
            }

            if(animator.GetBool("Die") == false)
            {
                var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
            }

            float currentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", currentSpeed);
            if (gameObject.name.Contains("Enemy1")) navMeshAgent.speed = 6.8f;
            else if (gameObject.name.Contains("Enemy2")) navMeshAgent.speed = 6.3f;
            else if (gameObject.name.Contains("Enemy3")) navMeshAgent.speed = 6.3f;
        } 
    }
}

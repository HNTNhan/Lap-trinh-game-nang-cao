using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private float attackRefreshRate = 1.5f;


    private AggroDetection aggroDetection;
    private Health healthTarget;
    private Animator animator;
    private float attackTimer;

    public GameObject player;

    private NavMeshAgent distance;
    private Vector3 playerPosition;
    bool check;
    bool attacked;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        attackTimer = 0;
        attacked = false;
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        playerPosition = target.GetComponent<Transform>().position;
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            healthTarget = health;
        }
    }

    private void Update()
    {
        transform.GetChild(0).GetComponent<Transform>().rotation = transform.rotation;
        if(healthTarget != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !attacked)
                {
                    if (Vector3.Distance(player.GetComponent<Transform>().position, transform.position) < distance.stoppingDistance)
                    {
                        Attack();
                        attacked = true;
                    }
                }
            }
            attackTimer += Time.deltaTime;
            if (CanAttack())
            {
                animator.SetTrigger("Attack");
                check = true;
                attacked = false;
                //if (distance.remainingDistance < distance.stoppingDistance)
                //{
                //    Attack();
                //}
            }
        }
    }

    private bool CanAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && check == true) return false;
        check = false;
        if (Vector3.Distance(player.GetComponent<Transform>().position, transform.position) > distance.stoppingDistance || distance.remainingDistance == 0)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle_attack"))
            {
                animator.Rebind();
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Normal");
            }
            return false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle_attack"))
        {
            animator.ResetTrigger("Normal");
            animator.SetTrigger("Attack");
            return false;
        }
        return true;
    }

    private void Attack()
    {
        attackTimer = 0;
        healthTarget.TakeDamage(1);
        //animator.SetBool("attack", true);
    }

    
}

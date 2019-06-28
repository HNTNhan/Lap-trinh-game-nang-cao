using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    //private float attackRefreshRate = 1.5f;

    private AggroDetection aggroDetection;
    private Player playerTarget;
    private Animator animator;
    public float checkTime;
    private GameObject player;
    private NavMeshAgent distance;
    bool check;
    bool attacked;
    private float difficulty;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        checkTime = 0;
        attacked = false;
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            playerTarget = player;
        }
    }

    private void Update()
    {
        if (playerTarget != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                checkTime += Time.deltaTime;
                if (checkTime >= 2)
                {
                    checkTime = 0;
                    GetComponent<Health>().Die();
                }
            }
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
        if (Vector3.Distance(player.GetComponent<Transform>().position, transform.position) > distance.stoppingDistance)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle_attack"))
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
        checkTime = 0;

        if (PlayerPrefs.GetString("Difficulty") == "easy") difficulty = 1;
        else if (PlayerPrefs.GetString("Difficulty") == "normal") difficulty = 1.5f;
        else if (PlayerPrefs.GetString("Difficulty") == "hard") difficulty = 2f;

        if (gameObject.name.Contains("Enemy3")) playerTarget.TakeDamage((int)Math.Round(3 * difficulty - 1));
        else if (gameObject.name.Contains("Enemy2")) playerTarget.TakeDamage((int)Math.Round(1 * difficulty));
        else if (gameObject.name.Contains("Enemy1")) playerTarget.TakeDamage((int)Math.Round(1 * difficulty));
        else if (gameObject.name.Contains("Enemy")) playerTarget.TakeDamage((int)Math.Round(2 * difficulty));
        //animator.SetBool("attack", true);
    }

    
}

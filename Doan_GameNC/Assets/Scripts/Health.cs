using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 5;
    private int currentHealth;
    private Animator animator;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        if(transform.GetChild(0).name == "Zombie") currentHealth = startingHealth;
        if (transform.GetChild(0).name == "Zombie1")
        {
            startingHealth -= 1;
            currentHealth = startingHealth;
        }
        if (transform.GetChild(0).name == "Zombie2")
        {
            startingHealth += 2;
            currentHealth = startingHealth;
        }
        if (gameObject.name == "Player")
        {
            startingHealth *= 2;
            currentHealth = startingHealth;
        }
        animator = GetComponentInChildren<Animator>();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        float currentHealthPct = (float)currentHealth / (float)startingHealth;
        OnHealthPctChanged(currentHealthPct);

        if(currentHealth <= 0)
        {
            if(gameObject.name == "Player")
            {
                Die();
                return;
            }
            animator.SetBool("Die", true);
            animator.ResetTrigger("Attack");
            //Die();
        }
    }

    public void Die()
    {
        if(gameObject.name == "Player")
        {
            animator.SetBool("Die", true);
        }
        else gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 5;
    private int currentHealth;
    private Animator animator;
    private float difficulty;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
        if (PlayerPrefs.GetString("Difficulty") == "easy") difficulty = 1;
        else if (PlayerPrefs.GetString("Difficulty") == "normal") difficulty = 1.5f;
        else if (PlayerPrefs.GetString("Difficulty") == "hard") difficulty = 2f;
        Debug.Log(PlayerPrefs.GetString("Difficulty"));
        if (transform.GetChild(0).name == "Zombie")
        {
            startingHealth = (int)Math.Round((startingHealth) * difficulty);
            currentHealth = startingHealth;
        }
        if (transform.GetChild(0).name == "Zombie1")
        {
            startingHealth = (int)Math.Round((startingHealth - 1) * difficulty);
            currentHealth = startingHealth;
        }
        if (transform.GetChild(0).name == "Zombie2")
        {
            startingHealth = (int)Math.Round((startingHealth + 1) * difficulty);
            currentHealth = startingHealth;
        }
        if (transform.GetChild(0).name == "Zombie3")
        {
            startingHealth = (int)Math.Round((startingHealth * 3) * difficulty);
            currentHealth = startingHealth;
        }
        
        if (gameObject.name == "Player")
        {
            startingHealth *= 8;
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

    public void IncreaseHealth(int health)
    {
        startingHealth += health;
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

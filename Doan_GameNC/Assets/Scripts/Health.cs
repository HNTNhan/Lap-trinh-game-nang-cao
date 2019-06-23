using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 5;
    private int currentHealth;
    private void OnEnable()
    {
        if(transform.GetChild(0).name == "Zombie") currentHealth = startingHealth;
        if(transform.GetChild(0).name == "Zombie1") currentHealth = startingHealth-1;
        if(transform.GetChild(0).name == "Zombie2") currentHealth = startingHealth+2;
        if (gameObject.name == "Player") currentHealth = startingHealth * 2;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}

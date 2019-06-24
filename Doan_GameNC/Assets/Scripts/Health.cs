using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 5;
    private int currentHealth;
    private Animator animator;

    private void OnEnable()
    {
        if(transform.GetChild(0).name == "Zombie") currentHealth = startingHealth;
        if(transform.GetChild(0).name == "Zombie1") currentHealth = startingHealth-1;
        if(transform.GetChild(0).name == "Zombie2") currentHealth = startingHealth+2;
        if (gameObject.name == "Player") currentHealth = startingHealth * 2;
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log(gameObject.name);
        currentHealth -= damageAmount;
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
        gameObject.SetActive(false);
    }
}

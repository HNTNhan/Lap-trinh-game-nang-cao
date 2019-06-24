using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 10;
    private int currentHealth;
    private int ammo = 20;
    private int maxAmmo = 60;
    private int currentAmmo;
    private Vector3 position;
    private Quaternion rotation;
    private Animator animator;

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        currentHealth = startingHealth;
        animator = GetComponentInChildren<Animator>();
        if (PlayerPrefs.GetString("Load") == "true") LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        currentHealth = data.health;
        maxAmmo = data.maxAmmo;
        currentAmmo = data.currentAmmo;

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;

        Quaternion rotation = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], 1);
        transform.rotation = rotation;

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }


}

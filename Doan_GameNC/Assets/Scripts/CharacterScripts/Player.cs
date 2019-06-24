using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;
    [SerializeField]
    private int maxAmmo = 60;
    private int score = 0;
    private int currentAmmo;
    private Vector3 position;
    private Quaternion rotation;
    private Animator animator;

    public int GetCurrentHealth()
    {
        return health.GetCurrentHealth();
    }
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetScore()
    {
        return score;
    }


    private void OnEnable()
    {
        Cursor.visible = false;
        health = GetComponent<Health>();
        currentAmmo = maxAmmo;
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
        health.SetCurrentHealth(data.health);
        maxAmmo = data.maxAmmo;
        currentAmmo = data.currentAmmo;

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;

        Quaternion rotation = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], 1);
        transform.rotation = rotation;

    }

    public void TakeDamage(int damageAmount)
    {
        health.TakeDamage(damageAmount);
        if(health.GetCurrentHealth() <= 0)
        {
            animator.SetBool("Die", true);
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    public void ReduceAmmo()
    {
        currentAmmo--;
    }

    public void IncreaseScore(int num)
    {
        score += num;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }


}

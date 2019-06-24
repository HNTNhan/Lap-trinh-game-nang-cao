using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float fireRate = 0.8f;

    [SerializeField]
    [Range(1,10)]
    private int damage = 1;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private AudioSource gunfireSound;

    private float timer;

    private bool isReloading = false;
    private float reloadTime = 3.1f;

    private Animator animator;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Die")) return;
        timer += Time.deltaTime;
        if(isReloading)
        {
            return;
        }
        if (player.GetCurrentAmmo() <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
        if (timer>=fireRate)
        {
            if(Input.GetButton("Fire1"))
            {
                timer = 0f;
                FireGun();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.Play("Reload");
        yield return new WaitForSeconds(reloadTime);

        player.Reload();
        isReloading = false;
    }

    private void FireGun()
    {
        if (PauseMenu.GameIsPause) return;
        muzzleParticle.Play();
        gunfireSound.Play();
        animator.Play("Shoot");

        player.ReduceAmmo();

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray,out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<Health>();
            
            if(health != null)
            {
                health.TakeDamage(damage);
                if(hitInfo.collider.tag=="Enemy")
                {
                    if(health.GetCurrentHealth() == 0)
                    {
                        player.IncreaseScore(100);
                    }
                }
            }
        }
    }
}

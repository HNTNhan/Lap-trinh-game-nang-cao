using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1f)]
    private float fireRate = 1f;

    [SerializeField]
    [Range(1,10)]
    private int damage = 1;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private AudioSource[] gunSound;

    private float timer;

    private bool isReloading = false;
    private float reloadTime = 3.1f;

    private Animator animator;

    private Player player;
    private GameObject[] allEnemy;
    private bool check;
    private float checkTime;

    private void Awake()
    {
        checkTime = 0;
        check = false;
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public int GetDamage()
    {
        return damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Die")) return;
        timer += Time.deltaTime;
        if(check) checkTime += Time.deltaTime;
        if (checkTime >= 5)
        {
            for (int i = 0; i < allEnemy.Length; i++)
            {
                allEnemy[i].GetComponent<SphereCollider>().radius = 20;
            }
            check = false;
        }
        if(isReloading || animator.GetBool("IsRunning"))
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
            if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {
                timer = 0f;
                FireGun();
            }
        }
    }

    public void DecreaseFireRate(float num)
    {
        fireRate = fireRate * (1 - num);
    }

    public void IncreaseDamge(int num)
    {
        damage += num;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.Play("Reload");
        gunSound[1].Play();
        yield return new WaitForSeconds(reloadTime);

        player.Reload();
        isReloading = false;
    }

    private void FireGun()
    {
        if (PauseMenu.GameIsPause) return;
        checkTime = 0;
        check = true;
        muzzleParticle.Play();
        gunSound[0].Play();
        animator.Play("Shoot");

        player.ReduceAmmo();

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 2f);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray,out hitInfo, 50))
        {
            var health = hitInfo.collider.GetComponent<Health>();

            for (int i = 0; i < allEnemy.Length; i++)
            {
                Debug.Log(Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, hitInfo.collider.GetComponent<Transform>().position));
                Debug.Log(hitInfo.collider.name);
                if(Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, hitInfo.collider.GetComponent<Transform>().position) >= 0 
                    && Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, hitInfo.collider.GetComponent<Transform>().position) < 15)
                {
                    allEnemy[i].GetComponent<SphereCollider>().radius = 60;
                }
                else if(Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, hitInfo.collider.GetComponent<Transform>().position) >= 15
                    && Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, hitInfo.collider.GetComponent<Transform>().position) < 30)
                {
                    allEnemy[i].GetComponent<SphereCollider>().radius = 30;
                }
                else if(Vector3.Distance(allEnemy[i].GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 50)
                {
                    allEnemy[i].GetComponent<SphereCollider>().radius = 30;
                }
            
            }

            if (health != null)
            {
                health.TakeDamage(damage);
                var enemy = hitInfo.collider;
                if(enemy.tag=="Enemy")
                {
                    if(health.GetCurrentHealth() == 0)
                    {
                        if(enemy.gameObject.name=="Enemy")
                            player.IncreaseScore(100);
                        if (enemy.gameObject.name == "Enemy1")
                            player.IncreaseScore(150);
                        if (enemy.gameObject.name == "Enemy2")
                            player.IncreaseScore(200);
                    }
                }
            }
        }
    }
}

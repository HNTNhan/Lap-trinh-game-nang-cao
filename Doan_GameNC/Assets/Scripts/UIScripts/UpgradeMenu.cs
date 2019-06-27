using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    Health playerHealth;

    Gun playerGun;

    // Start is called before the first frame update
    private void Awake()
    {
        playerHealth = player.GetComponent<Health>();
        playerGun = player.GetComponent<Gun>();
    }

    public void UpgradeHealth()
    {
        playerHealth.IncreaseHealth(2);
    }

    public void UpgradeFireRate()
    {
        playerGun.DecreaseFireRate(0.1f);
    }

    public void UpgradeDamage()
    {
        playerGun.IncreaseDamge(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

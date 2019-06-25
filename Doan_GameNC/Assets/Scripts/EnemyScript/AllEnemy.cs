using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemy : MonoBehaviour
{
    public GameObject[] allEnemy; 

    private void OnEnable()
    {
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (PlayerPrefs.GetString("Load") == "true") LoadEnemy();
    }

    public void SaveEnemy()
    {
        SaveSystem.SaveEnemy(this);

    }
    public void LoadEnemy()
    {
        EnemyData data = SaveSystem.LoadEnemy();

        for (int j = 0; j < allEnemy.Length; j++)
        {
            for (int i = 0; i < data.name.Length; i++)
            {
                if (allEnemy[j].name == data.name[i])
                {
                    allEnemy[j].SetActive(true);
                    allEnemy[j].GetComponent<Health>().SetCurrentHealth(data.health[i]);
                    allEnemy[j].GetComponent<Health>().TakeDamage(0);
                    allEnemy[j].GetComponent<Enemy>().checkTime = 0;
                    allEnemy[j].GetComponentInChildren<Animator>().Rebind();
                    Vector3 position = new Vector3(data.position[i, 0], data.position[i, 1], data.position[i, 2]);
                    allEnemy[j].GetComponent<Transform>().position = position;
                    break;
                }
                if (i + 1 >= data.name.Length)
                {
                    allEnemy[j].SetActive(false);
                }
            }
        }
    }
}

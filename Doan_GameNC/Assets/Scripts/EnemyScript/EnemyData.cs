using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float[,] position;
    public string[] name;

    public EnemyData(AllEnemy allEnemy)
    {
        position = new float[allEnemy.allEnemy.Length, 3];
        name = new string[allEnemy.allEnemy.Length];
        for(int i = 0; i < allEnemy.allEnemy.Length; i++)
        {
            if (!allEnemy.allEnemy[i].activeSelf || allEnemy.allEnemy[i].GetComponentInChildren<Animator>().GetBool("Die")) continue;
            Debug.Log(allEnemy.allEnemy[i].name);
            name[i] = allEnemy.allEnemy[i].name;
            position[i , 0] = allEnemy.allEnemy[i].transform.position.x;
            position[i , 1] = allEnemy.allEnemy[i].transform.position.y;
            position[i , 2] = allEnemy.allEnemy[i].transform.position.z;
        }
    }

}

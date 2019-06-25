using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float[] position;
    public float[] rotation;
    public int maxAmmo;
    public int currentAmmo;
    public int score;

    public PlayerData(Player player)
    {
        health = player.GetCurrentHealth();
        maxAmmo = player.GetMaxAmmo();
        currentAmmo = player.GetCurrentAmmo();
        score = player.GetScore();

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        rotation = new float[3];
        rotation[0] = player.transform.rotation.x;
        rotation[1] = player.transform.rotation.y;
        rotation[2] = player.transform.rotation.z;

    }


}

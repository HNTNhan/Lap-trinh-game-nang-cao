using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.fun";
        string path = "player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        //string path = Application.persistentDataPath + "/player.fun";
        string path = "player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void SaveEnemy(AllEnemy allEnemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.fun";
        string path = "enemy.fun";
        FileStream stream = new FileStream(path, FileMode.Create);


        EnemyData data = new EnemyData(allEnemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadEnemy()
    {
        //string path = Application.persistentDataPath + "/player.fun";
        string path = "enemy.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}

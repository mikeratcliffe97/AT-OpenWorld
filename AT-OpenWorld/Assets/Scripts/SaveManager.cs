using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static void SaveLevelData(Level level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/map.sav", FileMode.Create);

        SavedLevelData data = new SavedLevelData(level);

        bf.Serialize(stream, data);
        Debug.Log("SAveD");
        stream.Close();
    }

    public static void SavePlayerData(PlayerData player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        SavedPlayerData p_data = new SavedPlayerData(player);

        bf.Serialize(stream, p_data);
        Debug.Log("PSAved");
        stream.Close();
    }
    public static int[] LoadLevelData()
    {
        if (File.Exists(Application.persistentDataPath + "/map.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/map.sav", FileMode.Open);

            SavedLevelData data = bf.Deserialize(stream) as SavedLevelData;

            stream.Close();

            return data.dims;
        }

        else
        {
            Debug.Log("this ain't it chief");
            return null;

        }
    }



    public static float[] LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            SavedPlayerData p_data = bf.Deserialize(stream) as SavedPlayerData;

            stream.Close();

            return p_data.pos;
        }

        else
        {
            Debug.Log("this ain't it chief");
            return null;

        }
    }

}

[Serializable]
public class SavedLevelData
{

    public int[] dims;
    
    public SavedLevelData(Level level)
    {
        dims = new int[3];
        dims[0] = level.mapWidth;
        dims[1] = level.mapDepth;
        dims[2] = level.numberofTiles;
       

    }
}

[Serializable]
public class SavedPlayerData
{

    public float[] pos;

    public SavedPlayerData(PlayerData player)
    {
        pos = new float[3];
        pos[0] = player.playerX;
        pos[1] = player.playerY;
        pos[2] = player.playerZ;

    }
}
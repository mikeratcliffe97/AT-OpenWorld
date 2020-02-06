using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static  class SaveManager
{
  public static void SaveData(Level level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/map.sav", FileMode.Create);

        LevelData data = new LevelData(level);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/map.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/map.sav", FileMode.Open);

            LevelData data = bf.Deserialize(stream) as LevelData;

            stream.Close();

            return data.dims;
        }

        else
        {
            Debug.Log("this ain't it chief"); 
            return null;

        }
    }


}

[Serializable]
public class LevelData
{

    public int[] dims;

    public LevelData(Level level)
    {
        dims = new int[2];
        dims[0] = level.mapWidth;
        dims[1] = level.mapDepth;
    }
}
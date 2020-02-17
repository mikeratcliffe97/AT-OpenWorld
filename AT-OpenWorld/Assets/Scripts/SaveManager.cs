﻿using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{



    public static void SaveTileData(TileData tileData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/tile.sav", FileMode.Create);

        SavedTileData t_data = new SavedTileData(tileData);

        bf.Serialize(stream, t_data);
        Debug.Log("TSaved");

        Debug.Log(tileData);
        stream.Close();
    }

    public static float[,] LoadTileData()
    {
        if (File.Exists(Application.persistentDataPath + "/tile.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/tile.sav", FileMode.Open);

            SavedTileData t_data = bf.Deserialize(stream) as SavedTileData;

            stream.Close();

            return t_data.mapdata;
        }

        else
        {
            Debug.Log("this ain't it chief");
            return null;

        }
    }
    public static void SaveLevelData(Level level)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/map.sav", FileMode.Create);

        SavedLevelData data = new SavedLevelData(level);

        bf.Serialize(stream, data);
        Debug.Log("SAveD");
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

    public static void SavePlayerData(PlayerData player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        SavedPlayerData p_data = new SavedPlayerData(player);

        bf.Serialize(stream, p_data);
        Debug.Log("PSAved");
        stream.Close();
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

[Serializable]
public class SavedTileData
{

    public float[,] mapdata;


    public SavedTileData (TileData tileData)
    {
        mapdata = new float[7,2];
        //tile depth
        mapdata[0, 0] = tileData.heightMap[0, 0];
        //tile width
        mapdata[1, 0] = tileData.heightMap[1, 0];
        //mapscale
        mapdata[2, 0] = tileData.heightMap[2, 0];
        //offsetX
        mapdata[3, 0] = tileData.heightMap[3, 0];
        //offsetZ
        mapdata[4, 0] = tileData.heightMap[4, 0];
        //waves
        mapdata[5, 1] = tileData.heightMap[5, 1];

        //id
        mapdata[6, 0] = tileData.tileNumber;
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour

{
    // Start is called before the first frame update
    private LevelGenerator levelGen;
    public int mapWidth = 0;
    public int mapDepth = 0;
    public int numberofTiles = 0;

   

    public void Save()
    {
       
        SaveManager.SaveLevelData(this);
    }

    public void Load()
    {
        int[] loadedDims = SaveManager.LoadLevelData();
        mapWidth = loadedDims[0];
        mapDepth = loadedDims[1];
        numberofTiles = loadedDims[2];
        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();

        levelGen.setDims();
        levelGen.GenerateMap();
       // levelGen.SpawnPlayer();
       

    }
}

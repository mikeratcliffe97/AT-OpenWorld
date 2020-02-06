using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour

{
    // Start is called before the first frame update
    private LevelGenerator levelGen;
    public int mapWidth = 0;
    public int mapDepth = 0;

   
    public void Save()
    {
       
        SaveManager.SaveData(this);
    }


    public void Load()
    {
        int[] loadedDims = SaveManager.LoadData();
        mapWidth = loadedDims[0];
        mapDepth = loadedDims[1];
        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();

        levelGen.setDims();
        levelGen.GenerateMap();
       

    }
}

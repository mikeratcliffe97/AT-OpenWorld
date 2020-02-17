using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour

{
    // Start is called before the first frame update
    private LevelGenerator levelGen;
    [SerializeField]
    private GameObject levelTile;


    public int mapWidth = 0;
    public int mapDepth = 0;
    public int numberofTiles = 0;


    private void Awake()
    {
        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();
    }
    public void Save()
    {
       
        SaveManager.SaveLevelData(this);
    }

    public void Load()
    {
        int[] loadedDims = SaveManager.LoadLevelData();
        mapWidth = loadedDims[0];
        mapDepth = loadedDims[1];
     //   numberofTiles = loadedDims[2];
       

        levelGen.setDims();
        levelGen.GenerateMap();
       // levelGen.SpawnPlayer();
       
    }


    public void disableTiles()
    {

      //  if (levelTile.gameObject.GetComponent<TileGenerator>().tileNumber % 50 == 0 || levelTile.gameObject.GetComponent<TileGenerator>().tileNumber % 100 == 0)
        {

           // levelTile.GetComponent<TileData>().tileNumber
            //  Debug.Log("tile #" + levelTile.gameObject.GetComponent<TileGenerator>().tileNumber);
        }


    }
}


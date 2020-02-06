using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
   
    private Button newmap;
 
    [SerializeField]
    private int mapWidthInTiles, mapDepthInTiles;

    [SerializeField]
    private GameObject levelTile;

    private Level level;
   
    // Start is called before the first frame update
    void Start()
    {
        newmap = GameObject.Find("NewButton").GetComponent<Button>();
        newmap.onClick.AddListener(delegate { setRandDims();  GenerateMap(); });

        level = GameObject.Find("Level").GetComponent<Level>();
       // GenerateMap();
       
       
    }

    public void GenerateMap()
    {
        if (mapWidthInTiles > 0)
        {//get dims from prefab
            Vector3 tileSize = levelTile.GetComponent<MeshRenderer>().bounds.size;
            int tileWidth = (int)tileSize.x;
            int tileDepth = (int)tileSize.z;

            //make new tile in position
            for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
            {
                for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
                {
                    Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + xTileIndex * tileWidth,
                        this.gameObject.transform.position.y, this.gameObject.transform.position.z + zTileIndex * tileDepth);

                    GameObject tile = Instantiate(levelTile, tilePosition, Quaternion.identity) as GameObject;

                }
            }
        }
    }

    

    public int getWidth()
    {
        int mWidth = mapWidthInTiles;
        return mWidth;
    }

    public int getDepth()
    {
        int mDepth = mapDepthInTiles;
        return mDepth;
    }

    private void setRandDims()
    {
        int randWidth = Random.Range(1, 10);
        int randDepth = Random.Range(1, 10);
        mapWidthInTiles = randWidth;
        mapDepthInTiles = randDepth;

    
        level.mapWidth = mapWidthInTiles;
        level.mapDepth = mapDepthInTiles;
    }

    public void setDims()
    {
        mapWidthInTiles = level.mapWidth;
        mapDepthInTiles = level.mapDepth;
    }
}



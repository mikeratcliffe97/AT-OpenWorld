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

    public GameObject player;

    private Level level;
   

    // Start is called before the first frame update
    void Start()
    {
        newmap = GameObject.Find("NewButton").GetComponent<Button>();
        newmap.onClick.AddListener(delegate { setRandDims();  GenerateMap(); SpawnPlayer(); });

        level = GameObject.Find("Level").GetComponent<Level>();
        levelTile = GameObject.Find("LevelTile").GetComponent<GameObject>();
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
                    
                    level.numberofTiles++;
                    levelTile.gameObject.GetComponent<TileGenerator>().tileNumber = level.numberofTiles;
                }
            }
        }
        
    }

    public void SpawnPlayer()
    {
      
            Vector3 playerPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 5, this.gameObject.transform.position.z);
       
       GameObject currentPlayer =   Instantiate(player, playerPos, Quaternion.identity);
        currentPlayer.SetActive(true);
   
       
    }

    public void LoadPlayer(float xPos, float yPos, float zPos)
    {

        Vector3 playerPos = new Vector3(xPos, xPos, zPos);

        GameObject currentPlayer = Instantiate(player, playerPos, Quaternion.identity);
        currentPlayer.SetActive(true);

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
        int randWidth = Random.Range(5, 50);
        int randDepth = Random.Range(5, 50);
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



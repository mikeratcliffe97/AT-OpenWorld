
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class LevelGenerator : MonoBehaviour
{
    private Button newmap;
    private int MAXSHEEP = 50;
    [SerializeField]
    private int mapWidthInTiles, mapDepthInTiles;

    [SerializeField]
    private GameObject levelTile;

    public List<GameObject> allTiles;

    [SerializeField]
    private GameObject trough;

    [SerializeField]
    private GameObject sheepObj;

    public GameObject waypoint;

    public List<GameObject> pointsList;
    public GameObject player;

    private Level level;

    private GameObject tile;
    [SerializeField]
    private GameObject stream;
    // Start is called before the first frame update
    void Start()
    {
        newmap = GameObject.Find("NewButton").GetComponent<Button>();
        newmap.onClick.AddListener(delegate { GenerateMap(); SpawnPlayer(); SpawnSheep(); });

        level = GameObject.Find("Level").GetComponent<Level>();
        pointsList = new List<GameObject>();
       
       // GenerateMap();
        //SpawnPlayer();
    }

    public void GenerateMap()
    {
       
        allTiles = new List<GameObject>();
        int[] troughTileNumber = new int[249];

        for (int i = 0; i < troughTileNumber.Length; i++)
        {
            troughTileNumber[i] = Random.Range(0, 250);
        }
       
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
                    Vector3 tilePosition = new Vector3(
                        this.gameObject.transform.position.x + xTileIndex * tileWidth,
                        this.gameObject.transform.position.y, 
                        this.gameObject.transform.position.z + zTileIndex * tileDepth);

                    //Spawn new tile
                    tile = Instantiate(levelTile, tilePosition, Quaternion.identity) as GameObject;
                   
                    //attach tile to level
                    tile.transform.SetParent(level.transform);
                    //Increase number of tiles in level
                    level.numberofTiles++;
                    //Assign tile number according to total number of tiles
                    levelTile.gameObject.GetComponent<TileData>().tileNumber = level.numberofTiles;
                   //Rename tile for ease
                    levelTile.name = "levelTile: " + level.numberofTiles;
                    levelTile.tag = ("Tile");
                    
                    System.Array.Sort(troughTileNumber);
                    levelTile.gameObject.GetComponent<TileData>().isWaypoint = System.Array.Exists(troughTileNumber, element  => element == levelTile.gameObject.GetComponent<TileData>().tileNumber);
                    //if tile has a waypoint (randomly assigned) spawn a point on it
                    if (levelTile.gameObject.GetComponent<TileData>().isWaypoint)
                    {
                        Vector3 troughPos = new Vector3(Random.Range(0, 490), this.transform.position.y + 5, Random.Range(0, 490));
                        SpawnTrough(troughPos);
                        waypoint.transform.SetParent(level.transform);
                    }
                    if (level.numberofTiles == 2500 || level.numberofTiles == 5000)
                    {
                        levelTile.name = "levelTile: 0";
                    }
                    //levelTile.gameObject.GetComponent<TileData>().SaveTile();
                    allTiles.Add(tile);
                }
                 
            }
                  
            level.mapWidth = mapWidthInTiles;
            level.mapDepth = mapDepthInTiles;
        }

    }

    public void SpawnPlayer()
    {
        //Spawn player object
        Vector3 playerPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 20, this.gameObject.transform.position.z);

        GameObject currentPlayer = Instantiate(player, playerPos, Quaternion.identity);
        currentPlayer.transform.SetParent(level.transform);


    }

    public void SpawnSheep()
    {
        for (int i = 0; i < MAXSHEEP; i++)
        {
            int xPos = Random.Range(5, 479);
            int zPos = Random.Range(5, 479);
            //Allows sheep to spawn above mountain
            Vector3 sheepPos = new Vector3(xPos, 20, zPos);
            GameObject sheep = Instantiate(sheepObj, sheepPos, sheepObj.transform.rotation);
            sheep.transform.SetParent(level.transform);
        }
    }

    public void SpawnTrough(Vector3 position)
    {
            pointsList.Add(waypoint = Instantiate(trough, position, Quaternion.identity) as GameObject);
            waypoint.tag = "Waypoint";
           // waypoint.SetActive(false);
    }

    public void LoadPlayer(float xPos, float yPos, float zPos)
    {

        Vector3 playerPos = new Vector3(xPos, yPos + 20, zPos);

        GameObject currentPlayer = Instantiate(player, playerPos, Quaternion.identity);

        currentPlayer.transform.SetParent(level.transform);
        player = currentPlayer.GetComponent<GameObject>();
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




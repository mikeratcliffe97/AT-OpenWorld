using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    private int mapWidthInTiles, mapDepthInTiles;

    [SerializeField]
    private GameObject levelTile;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();    
    }

       void GenerateMap()
    {
        //get dims from prefab
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

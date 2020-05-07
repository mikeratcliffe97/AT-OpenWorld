using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStreamer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private Level level;
    [SerializeField]
    private List<GameObject> tiles;
    [SerializeField]
    private LevelGenerator levelGen;
    private bool tilesAssigned = false;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
       // player = playerObj.GetComponent<Transform>();
        //Init list
        tiles = new List<GameObject>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
            player = playerObj.GetComponent<Transform>();
        }

      if (!tilesAssigned)
        {
            AssignTiles();
        }

        foreach(GameObject activeTile in tiles)
        {
            //check tile position against player
           if (activeTile.transform.position.x >= player.position.x + 100 || activeTile.transform.position.z >= player.position.z + 100)
            {
                activeTile.SetActive(false);
            }

           else if(activeTile.transform.position.x <= player.position.x - 100 || activeTile.transform.position.z <= player.position.z - 100)
            {
                activeTile.SetActive(false);
            }
           else
            {
                activeTile.SetActive(true);
            }
        }
    }

    void AssignTiles()
    {
       //Fill list as we can't on load
        tiles.AddRange(levelGen.allTiles);
        tilesAssigned = true;
    }
    void ToggleActive()
    {
      
    }
}



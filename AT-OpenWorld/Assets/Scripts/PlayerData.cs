using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelGenerator levelGen;
    public float playerX = 0;
    public float playerY = 0;
    public float playerZ = 0;

    // Update is called once per frame
    private void Start()
    {
     //   playerObject = GameObject.Find("RigidBodyFPSController").GetComponent<GameObject>();
       // characterController = playerObject.GetComponent<CharacterController>();
    }

    public void Save()
    {
        playerX = this.transform.position.x;
        playerY = this.transform.position.y + 5;
        playerZ = this.transform.position.z;

        Debug.Log(playerX);
        SaveManager.SavePlayerData(this);
    }

    public void Load()
    {
        int[] loadedPos = SaveManager.LoadPlayerData();
        playerX = (float)loadedPos[0];
        playerY = (float)loadedPos[1];
        playerZ = (float)loadedPos[2];

        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();
        levelGen.LoadPlayer(playerX, playerY, playerZ);

    }


}

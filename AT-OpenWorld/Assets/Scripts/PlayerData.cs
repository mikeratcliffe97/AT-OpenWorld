using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    
    private LevelGenerator levelGen;
    
    private Button savebutton;


    [SerializeField]
    public float playerX, playerY, playerZ;
  

    // Update is called once per frame
    private void Start()
    {
        //   playerObject = GameObject.Find("RigidBodyFPSController").GetComponent<GameObject>();
        // characterController = playerObject.GetComponent<CharacterController>();
        savebutton = GameObject.Find("SaveButton").GetComponent<Button>();
        savebutton.onClick.AddListener(delegate { Save(); });
        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();
    }

    void Update()
    {
        playerX = this.transform.position.x;
        playerY = this.transform.position.y;
        playerZ = this.transform.position.z;

     //   levelGen.disableTiles();
    
    }

    public void Save()
    {

        Debug.Log(playerX);
        Debug.Log(playerY);
        Debug.Log(playerZ);
        SaveManager.SavePlayerData(this);
    }

    public void Load()
    {
        float[] loadedPos = SaveManager.LoadPlayerData();
        playerX = (float)loadedPos[0];
        playerY = (float)loadedPos[1];
        playerZ = (float)loadedPos[2];

        levelGen = GameObject.Find("Level").GetComponent<LevelGenerator>();
        levelGen.LoadPlayer(playerX, playerY, playerZ);

    }

   

}

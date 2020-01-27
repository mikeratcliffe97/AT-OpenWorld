using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnObjs : MonoBehaviour
{
    public GameObject game;
    public GameObject newgame;
    int newX = 0;
    float currentX = 0;
    int newZ = 0;
    int entrycount = 0;


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "MyTag")
        {
            entrycount = entrycount + 1;
            currentX = game.transform.position.x;
            newX = Mathf.RoundToInt(currentX);

            newZ = Random.Range(0, 10);



            newX = newX + 10;
            newgame = Instantiate(game,  new Vector3(newX, 0, newZ), new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0)) as GameObject;
            //   game.GetComponent<Rigidbody>().AddForce(new Vector3(myForceX, myForceY, myForceZ) * mySpeed)

            if (entrycount >= 2)
            {
                Destroy(game);
            }
        }
    }


    void OnTriggerExit(Collider col)
    {
        Destroy(game);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> waypoints;
    
    private GameObject NPC;
    [SerializeField]
    private LevelGenerator level;
    private float movement;
    
    public float Speed = 10f;

    private float startTime;

    int current_point = 0; 
    public bool running = false;
    private IEnumerator coroutine;

    GameObject nextTarget;
    private float distance;
    void Start()
    {
        waypoints = new List<GameObject>();
        startTime = Time.time;
        level = GameObject.Find("Level").GetComponent<LevelGenerator>();
        for (int i = 0; i < level.pointsList.Count; i++)
        { 
            waypoints.Add(level.pointsList[i]);
        }
        coroutine = StartMoving();
        StartCoroutine(coroutine);
        Debug.Log("Invoked");
    }

    // Update is called once per frame
    void Update()
    {
        movement = (Speed * Time.deltaTime);
        distance = Vector3.Distance(this.transform.position, nextTarget.transform.position);
        if (distance < 20)
        {
            int newPoint = SelectPoint(current_point);
            nextTarget = waypoints[newPoint];
            //Debug.Log(newPoint);
        }

        this.transform.position = Vector3.MoveTowards(this.transform.position, nextTarget.transform.position, movement);
    }

    void InitTarget()
    {
            nextTarget = (waypoints[SelectPoint(0)]);
            
             // distance = Vector3.Distance(this.transform.position, nextTarget.transform.position);
            //    float distCovered = ((Time.time - startTime) * Speed);
            //    float fractOfDistance = distCovered / distance;
            //  //  //this.transform.position = Vector3.Lerp(this.transform.position, nextTarget.transform.position, 1);

            //   // if (distance < 10)
            //    {
            //        nextTarget = waypoints[SelectPoint(waypoints.Count)].GetComponent<GameObject>();
            //        compareme = new Vector3(nextTarget.transform.position.x, nextTarget.transform.position.y, nextTarget.transform.position.z);
            //        Debug.Log(compareme + "new");
            //    }
            //this.transform.position = Vector3.MoveTowards(this.transform.position, nextTarget.transform.position, movement);
        
   // Debug.Log("Moved");

    }
    IEnumerator StartMoving()
    {
        Invoke("InitTarget", 1);
        yield return new WaitForSeconds(1);
    }

    
   private int SelectPoint(int pastPoint)
    {
//Allocates a random waypoint as next target
    int nextPoint = Random.Range(0, waypoints.Count);
      
     //Checks if waypoint is the same as current point
        if (nextPoint != pastPoint)
        {
            return nextPoint;
        }
        //if it is, assign a different point
        else
        {
            return nextPoint + 5;
        }
    }
}

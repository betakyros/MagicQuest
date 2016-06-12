
using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour
{
    //stolen from http://forum.unity3d.com/threads/a-waypoint-script-explained-in-super-detail.54678/

    public int speed = 0;
    
    private Transform waypoint;
    
    public Transform[] waypoints;

    private int WPindexPointer;
    
    void Start()
    {
        if(waypoints.Length <= 0)
        {
            print("set at least one waypoint on " + this);
        }
        WPindexPointer = 0;
        GetComponent<Rigidbody2D>().velocity = (speed * (getDirectionToCurrentWaypoint()));
    }

    //The function "OnTriggerEnter" is called when a collision happens.
    void OnTriggerStay2D()
    {
        if ((new Vector2(GetComponent<Renderer>().bounds.center.x, GetComponent<Renderer>().bounds.center.y) - new Vector2(waypoints[WPindexPointer].position.x, waypoints[WPindexPointer].position.y) ).magnitude < .2)
        {
            // When the GameObject collides with the waypoint's collider,
            // change the active waypoint to the next one in the array variable "waypoints".
            WPindexPointer++;

            // When the array variable reaches the end of the list ...
            if (WPindexPointer >= waypoints.Length)
            {
                // ... reset the active waypoint to the first object in the array variable
                // "waypoints" and start from the beginning.
                WPindexPointer = 0;
            }


            GetComponent<Rigidbody2D>().velocity = (speed * (getDirectionToCurrentWaypoint()));
        }
    }

    //returns a unit length vector in the direction of the next waypoint
    private Vector3 getDirectionToCurrentWaypoint()
    {
        Vector3 myPos = new Vector3(GetComponent<Renderer>().bounds.center.x, GetComponent<Renderer>().bounds.center.y);
        Vector3 waypointPos = waypoints[WPindexPointer].position;

        Vector3 directionToNextWaypoint = (waypointPos - myPos).normalized;

        return directionToNextWaypoint;
    }
}
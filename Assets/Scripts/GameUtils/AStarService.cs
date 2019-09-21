using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AStarService : MonoBehaviour
{
    //A* properties
    public float nextWaypointDistance = 3f;
    public float stoppingDistanceForTarget = 10f;
    public float retreatDistanceForTarget = 5f;

    public Path path;
    public Seeker seeker;
    public Transform target;
    public Transform finder;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(finder.position, target.position, OnPathComplete);
        }
    }
}

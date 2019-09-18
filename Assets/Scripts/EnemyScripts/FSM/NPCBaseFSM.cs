using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCBaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    public float speed = 2.0f;
    public float accuracy = 3.0f;

    //A* properties
    public float nextWaypointDistance = 3f;
    public float stoppingDistanceForTarget = 10f;
    public float retreatDistanceForTarget = 5f;

    Path path;
    Seeker seeker;
    int currentWaypoint;
    bool reachedEndOfPath;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = GameManager.Instance.player;
    }
}

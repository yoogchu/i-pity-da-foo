using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIRunScript : MonoBehaviour {
    public GameObject[] waypoints;
    public int currWaypoint = -1;
    public enum AIState
    {
        PatrolWaypoints,
        RunFromProjectile
    }
    public AIState aiState;
    public float pauseTime;
    public float switchProb;
    public CharShootingScript shootScript;

    private NavMeshAgent navMesh;
    private Animator anim;
    public bool pause;
    private float timePause = 0;

    private Vector3 charPos;
    private Vector3 charForward;
    private Vector3 aiPos;

    // Use this for initialization
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetNextWaypoint();
        aiState = AIState.PatrolWaypoints;
    }

    void Update()
    {
        switch (aiState)
        {
            case AIState.PatrolWaypoints:
                if (navMesh.remainingDistance <= 1.0f && !pause)
                {
                    pause = true;
                    timePause = Time.time;
                }
                if (pause && Time.time - timePause > pauseTime)
                {
                    SetNextWaypoint();
                }

                // Check that character fired in view of ai
                if (shootScript.justFired)
                {
                    charPos = shootScript.releasePoint.position;
                    charForward = shootScript.releasePoint.forward;
                    aiPos = transform.position;

                    // If ai looking within 30 degrees of char, then run.
                    float angle = Vector3.Angle(charPos - aiPos, transform.forward);
                    Debug.Log(angle);

                    if (angle <= 45.0f)
                    {
                        aiState = AIState.RunFromProjectile;
                    }
                    shootScript.justFired = false;
                }
                break;
            case AIState.RunFromProjectile:
                // Have AI run away from the expected projectile direction
                float dist = (charPos - aiPos).magnitude;

                Vector3 projDest = charForward * dist;
                Vector3 destination = aiPos - projDest;
                Vector3 newVector = Vector3.Cross(projDest, Vector3.up);
                newVector.Normalize();

                Vector3 aiDest = (aiPos + newVector * 10.0f - projDest).magnitude >
                    (aiPos + newVector * -10.0f - projDest).magnitude ? aiPos + newVector * 10.0f : aiPos + newVector * -10.0f;
                navMesh.SetDestination(aiDest);
                navMesh.acceleration = 500;
                navMesh.speed = 10;

                if (navMesh.remainingDistance <= 1.0f && !pause)
                {
                    pause = true;
                    timePause = Time.time;
                }
                if (pause && Time.time - timePause > pauseTime)
                {
                    navMesh.acceleration = 8;
                    navMesh.speed = 3.5f;
                    aiState = AIState.PatrolWaypoints;
                }
                break;
            default:
                break;
        }

        //anim.SetFloat("vely", navMesh.velocity.magnitude / navMesh.speed);
    }

    // Set Next Waypoint given current
    private void SetNextWaypoint()
    {
        if (waypoints.Length == 0)
            Debug.LogError("No Waypoints found.");

        pause = false;

        if (UnityEngine.Random.Range(0f,1f) <= switchProb)
        {
            currWaypoint++;
            if (currWaypoint == waypoints.Length)
            {
                currWaypoint = 0;
            }
        } else
        {
            currWaypoint--;
            if (currWaypoint < 0)
            {
                currWaypoint = waypoints.Length - 1;
            }
        }
        
        navMesh.SetDestination(waypoints[currWaypoint].transform.position);
    }
}

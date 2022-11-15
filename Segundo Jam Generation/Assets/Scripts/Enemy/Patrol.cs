using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] wayPoints;
    private int nextWayPoint;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
            Ended();
        }
    }
    private void OnEnable()
    {
        Ended();
    }

    void Ended()
    {
        agent.SetDestination(wayPoints[nextWayPoint].position);
    }
}

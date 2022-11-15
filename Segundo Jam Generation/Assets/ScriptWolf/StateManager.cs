using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{   
    //Brain
    public GameObject playerPosition;
    public State currenState;
    public AttackState attack;
    public IdleState idle;
    public float healt = 100;
    public Animator anim;
    public NavMeshAgent wolf;
    public Transform[] pointsWolf;
  
    

        void Start()
    {
        wolf = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }
  
    void Update()
    {
        RunstateMachine();

       // _wolf.SetDestination(point.position);
    }

    private void RunstateMachine()
    {
        State nextState = currenState?.RunCurrentState();

        if(nextState != null)
        {
           SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currenState = nextState;
       
        if (nextState == idle)
        {
            idle.UpdateDestination();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player") && playerPosition == null )
        {   
          playerPosition = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player") && playerPosition != null )
        {   
          playerPosition = null;
        }
    }

}

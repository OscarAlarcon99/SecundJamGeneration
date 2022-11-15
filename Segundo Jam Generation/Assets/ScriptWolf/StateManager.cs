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
    public float healt;
    public bool damage;
    public Animator anim;
    public NavMeshAgent wolf;
    public Transform[] pointsWolf;
    public DamageState damageState;
    public bool isActivate;
    
  
        void Start()
    {
        wolf = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        healt = 100;
        
    }
  
    void Update()
    {   
        if(!isActivate)
        {
            return;
        }
        RunstateMachine();
        if(Input.GetKeyDown(KeyCode.G))
        {
            damage = true;
            healt -= 20;
            anim.SetBool("Damage", true);
        }
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

    public void SwitchToTheNextState(State nextState)
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

    private void FixedUpdate()
    {  
        if (healt <= 0)
        {
            Death();
        }
    }
     void Death()
    {   

        anim.SetTrigger("Death");
        isActivate =false;
        
    }

}

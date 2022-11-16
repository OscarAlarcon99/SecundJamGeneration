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
    public DamageState damageState;
    public float healt;
    public bool isActivate;
    public bool damage;
    public Animator anim;
    public NavMeshAgent wolf;
    public Transform[] pointsWolf;
    public GameObject pointAttack;
    public Foot foot;

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
            currenState = null;
            return;
        }

        if (healt <= 0)
        {
            Debug.Log("mori");
            Death();
        }

        RunstateMachine();
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

    public void ChangeAttackPointStateTrue()
    {
        pointAttack.SetActive(true);
    }

    public void ChangeAttackPointStateFalse()
    {
        pointAttack.SetActive(false);
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
     void Death()
    {
        SoundManager.Instance.PlayNewSound("DeathEnemy");
        anim.SetTrigger("Death");
        ChangeAttackPointStateFalse();
        isActivate =false;
        foot.enabled = true;
        this.enabled = false;
    }
}

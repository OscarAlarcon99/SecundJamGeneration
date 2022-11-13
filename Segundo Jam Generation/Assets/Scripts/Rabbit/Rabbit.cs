using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject Player;
    public float EnemyDistanceRun = 18.0f;
    public float preventionDistance = 50.0f;
    private Animator anim;
    public Transform arrive;
    //public bool isActivated;
    public GameObject rabbitOff;
    

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //isActivated = true;
    }

   
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
          

        //Corre del player
        if(distance < EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
            anim.SetFloat("MoveSpeed", 1);

          
            
        }

        if (distance > preventionDistance)
        {
             _agent.SetDestination(arrive.position);
        }

      

   
 
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag== "Player")
        {   
            Die();
            //EnemyDistanceRun = 0;
            //isActivated =false;
            //gameObject.SetActive(false);
            this.enabled = false;
          
            
        }

        
    }

    public void Die()
    {
        //isActivated = false;
        anim.SetBool("Dead", true);

    }
}

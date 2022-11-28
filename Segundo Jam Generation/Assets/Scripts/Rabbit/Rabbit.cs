using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Foot foot;
    public GameObject player;
    public float EnemyDistanceRun = 18.0f;
    public float preventionDistance = 50.0f;
    private Animator anim;
    public Transform arrive;
    public GameObject rabbitOff;
    float distance;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
        //Corre del player
            if (distance < EnemyDistanceRun)
            {
                if (!Player.Instance.isStealth)
                {
                    Vector3 dirToPlayer = transform.position - player.transform.position;
                    Vector3 newPos = transform.position + dirToPlayer;
                    _agent.SetDestination(newPos);
                    anim.SetFloat("MoveSpeed", 1);
                }
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, arrive.position);

            if (distance > preventionDistance)
            {
                _agent.SetDestination(arrive.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag== "Player")
        {
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }
    
    public void Die()
    {
        anim.SetBool("Dead", true);
        _agent.Stop();
        _agent.ResetPath();
        SoundManager.Instance.PlayNewSound("DeathRabbit");
        foot.enabled = true;
        this.enabled = false;
    }
}

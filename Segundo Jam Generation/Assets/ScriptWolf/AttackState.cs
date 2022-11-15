using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{   
    
    public float waitAttackTime = 2;
    public float currentAttackTime;
    public override State RunCurrentState()
    {   
      if(brain.playerPosition != null || brain.damage)
      {
        currentAttackTime++;
        brain.wolf.SetDestination(brain.playerPosition.transform.position);

        if(Vector3.Distance(transform.position, brain.playerPosition.transform.position) < brain.wolf.stoppingDistance)
        {
          Attack();  
        }

         return this;
      }
      else
      {
        if(brain.damage)
        {       
            brain.damage = false;
             return brain.damageState;
        }
       
       else
       {
            return brain.idle;
       }
      }
      
       
    }

    void Attack()
    {
        if(currentAttackTime > waitAttackTime)
        {
            Debug.Log("Ssssss");
            brain.anim.SetTrigger("Attack");
            currentAttackTime = 0;
        }

    }
}

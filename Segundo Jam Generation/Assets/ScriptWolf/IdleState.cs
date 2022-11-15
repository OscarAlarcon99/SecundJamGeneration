using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{   
   
    
    int waypointIndex;
    Vector3 target;

    void Start(){
        UpdateDestination();
    }

    public void triggerPoint()
    {   
          if(Vector3.Distance(transform.position, target) < 1)
        {
            IteratedWaypointIndex();
            UpdateDestination();
        }
         
    }
    public override State RunCurrentState()
    {   
        triggerPoint(); 
     
        if(brain.playerPosition != null)
        {
            if(!Player.Instance.isStealth)
            {
                return brain.attack;
            }
            
            else
            {
                return this;
            }
            
        }
        else
        {
             return this;
        }
      
    }
    void UpdateDestination()
    {
        target = brain.pointsWolf[waypointIndex].position;
        brain.wolf.SetDestination(target);
    }
    void IteratedWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == brain.pointsWolf.Length)
        {
            waypointIndex = 0;
        }
    }
}

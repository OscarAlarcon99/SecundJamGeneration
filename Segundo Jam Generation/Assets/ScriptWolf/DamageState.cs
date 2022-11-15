using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
   public override State RunCurrentState()
   {
    
        if(!brain.anim.GetBool("Attack"))
        {
            return brain.attack;
            
        }
        else
        {
            return this;
        }
   }
   
   
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackPoint"))
        {   
           // brain.SwitchToTheNextState(this);
            brain.damage = true;
            brain.healt -= 50;
            brain.anim.SetBool("Damage", true);
        }
    }
}

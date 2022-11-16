using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    public float time;
    public float maxTime;

   public override State RunCurrentState()
   {
        time++;

        if (!brain.anim.GetBool("Damage") && time > maxTime)
        {
            Debug.Log("entro");
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
            SoundManager.Instance.PlayNewSound("DamageEnemy");
            time = 0;
            brain.SwitchToTheNextState(this);
            brain.healt -= 34;
            brain.anim.SetBool("Damage", true);
        }
    }
}

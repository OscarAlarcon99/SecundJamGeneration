using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerMovement moveController;
    public PlayerAnimation animationController;
    /// <summary>
    /// Controlador de camara de cinemachine
    /// </summary>
    public CinemachineControllerCamera cinemachineCamera;
    public PlayerHealt healt;
    public float timeWaitingDamage = 5;
    public bool isInvulnerable;
    public bool IsActive;
    public int currentTimeSpawn;
    public int timeAttack;

    
    private void Update()
    {
        if (IsActive)
            return;
        
        moveController.HandleActions();

        if (healt.life <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        animationController.SendAnimationReaction(2);
        IsActive = false;
    }

    public IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(timeWaitingDamage);
        isInvulnerable = false;
    }
}

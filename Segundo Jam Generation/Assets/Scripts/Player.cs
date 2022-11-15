using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [Header("PlayerComponents")]
    public PlayerHealt healtController;
    public PlayerMovement moveController;
    public PlayerAnimation animationController;
    /// <summary>
    /// Controlador de camara de cinemachine
    /// </summary>
    public VFXController vFXController;

    [Header("PlayerStats")]
    public float timeWaitingDamage = 1f;

    [Header("PlayerStatesMachine")]
    public bool isInteracting;
    public bool attackAir;
    public bool isStealth;
    public bool canDoCombo;
    public bool isInvulnerable;
    public bool isActive;
    public bool isDying;

    private void FixedUpdate()
    {
        if (!isActive)
            return;

        moveController.DirectUpdate();

        if (healtController.healt <= 0)
        {
            Death();
        }
    }

    private void Update()
    {
        isInteracting = animationController.m_animator.GetBool("IsInteracting");
        canDoCombo = animationController.m_animator.GetBool("CanDoCombo");
        animationController.m_animator.SetBool("Stealth", isStealth);
        attackAir = animationController.m_animator.GetBool("AttackAir");
        moveController.HandleActions();
    }

    void Death()
    {
        animationController.m_animator.SetBool("Death",true);
        isActive = false;
    }

    public IEnumerator DamagePlayer()
    {
        animationController.m_animator.SetTrigger("Damage");
        animationController.m_animator.SetBool("IsInteracting", true);
        vFXController.SettupEffectHealth();
        yield return new WaitForSeconds(timeWaitingDamage);
        isInvulnerable = false;
    }
}

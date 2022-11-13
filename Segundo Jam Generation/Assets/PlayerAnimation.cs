using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    /// <summary>
    /// Controlador de animaciones
    /// </summary>
    public Animator m_animator;
    
    public void SendAnimationReaction(int index)
    {
        if (index == 0)
        {
            m_animator.SetTrigger("Damage");
        }

        if (index == 1)
        {
            m_animator.SetBool("IsInteracting", true);
            m_animator.SetTrigger("Attack");
            Player.Instance.currentTimeSpawn = 0;
        }

        if (index == 2)
        {
            m_animator.SetBool("IsInteracting", true);
            m_animator.SetTrigger("Dead");
        }

        if (index == 3)
        {
            m_animator.SetBool("IsInteracting", true);
            m_animator.SetTrigger("IsRolling");
            m_animator.applyRootMotion = true;
        }
    }

    public void OnAnimatorMove()
    {
        if (!m_animator.GetBool("IsInteracting"))
            return;

        float delta = Time.deltaTime;
        Player.Instance.moveController.m_rigidBody.drag = 0;
        Vector3 deltaPosition = m_animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 _velocity;
        _velocity = (deltaPosition / delta) * Player.Instance.moveController.speedRoll;
        Player.Instance.moveController.m_rigidBody.velocity = _velocity;
    }
}

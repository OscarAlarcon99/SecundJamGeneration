using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    /// <summary>
    /// Controlador de animaciones
    /// </summary>
    public Animator m_animator;
    
    public void CanDoComboState(int value)
    {
        if (value == 0)
        {
            m_animator.SetBool("CanDoCombo", false);

        }

        else if (value == 1)
        {
            m_animator.SetBool("CanDoCombo", true);

        }
    }

    //public void OnAnimatorMove()
    //{
    //    if (!m_animator.GetBool("IsInteracting") && m_animator.applyRootMotion)
    //        return;

    //    float delta = Time.deltaTime;
    //    Player.Instance.moveController.m_rigidBody.drag = 0;
    //    Vector3 deltaPosition = m_animator.deltaPosition;
    //    deltaPosition.y = transform.position.y;
    //    Vector3 _velocity;
    //    _velocity = (deltaPosition / delta) * Player.Instance.moveController.speeIsInteracting;
    //    Player.Instance.moveController.m_rigidBody.velocity = _velocity;
    //}
}

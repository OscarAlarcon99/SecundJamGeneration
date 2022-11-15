using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Rabbit : MonoBehaviour
{

    private bool IsDead = false;
    public NavMeshAgent agent;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_RunCycleLegOffset = 0.2f;
    //float m_ForwardAmount;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    private Rigidbody m_Rigidbody;
    public SphereCollider m_Capsule;
    private Animator m_animator;
    
    //private int health = 1;
    public Transform[] wayPoints;
    private int nextWayPoint;
    Vector3 m_CapsuleCenter;
    Vector3 m_GroundNormal;
    Collider colliders;
    float m_OrigGroundCheckDistance;
    [SerializeField] float m_GroundCheckDistance = 0.2f;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //colliders = gameObject.GetComponentsInChildren<Collider>();
    }

    //agent.updateRotation = false;
    void Start()
    {
        m_Capsule = GetComponent<SphereCollider>();
        m_CapsuleCenter = m_Capsule.center;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
        m_animator.SetFloat("Forward", 1);
        //m_animator.SetFloat("Forward2", 2);
    }


    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
            m_animator.SetFloat("Forward", 0); // enemy walk
            m_animator.SetFloat("Forward", 1); // enemy walk
            Ended();
        }
        //PPRUEBA DE MUERTE
        /*if (Input.GetMouseButtonDown(0))
        {
            Die(0);
        }*/
    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;
        ApplyExtraTurnRotation();
        //UpdateAnimator(move);
    }
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            //m_IsGrounded = true;
            m_animator.applyRootMotion = true;
        }
        else
        {
            //m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_animator.applyRootMotion = false;
        }
    }
    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        //m_animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        //m_Animator.SetBool("Crouch", m_Crouching);
        //m_Animator.SetBool("OnGround", m_IsGrounded);
        /*if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }*/

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        /*if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }*/
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            Debug.Log("Player attack");
            m_animator.SetFloat("Forward", 1); // Enemy attack
           

        }

        if (!IsDead && agent.remainingDistance > agent.stoppingDistance)
        {
            Move(agent.desiredVelocity, false, false);
        }


    }
    public void Die(int health)
    {
        //Aqui disminuye la vida, revibe 3 golpes y muere.

        if (health == 0)
        {
            Debug.Log("Enemy die");

            m_animator.SetFloat("Forward", 2);
            //m_animator.SetFloat("Forward2", 4);
            IsDead = true;
            agent.enabled = false;
            this.enabled = false;
        }
    }
    private void OnEnable()
    {
        Ended();
    }
    void Ended()
    {
        agent.SetDestination(wayPoints[nextWayPoint].position);
    }
    void ApplyExtraTurnRotation()
    {

        transform.Rotate(0, m_TurnAmount * Time.deltaTime, 0);
    }
}

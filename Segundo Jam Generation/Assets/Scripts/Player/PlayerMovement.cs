using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Variable que almacena la velocidad del movimiento
    /// </summary>
    [SerializeField] 
    private float moveSpeed = 12;
    /// <summary>
    /// Variable que almacena la fuerza del salto
    /// </summary>
    [SerializeField] 
    private float m_jumpForce = 4;
    /// <summary>
    /// Cuerpo Rigido del player
    /// </summary>
    public Rigidbody m_rigidBody = null;

    /// <summary>
    /// Variable que almacena la anterior velocidad vertical
    /// </summary>
    private float m_currentV = 0;

    /// <summary>
    /// Variable que almacena la anterior velocidad horizontal
    /// </summary>
    private float m_currentH = 0;
    /// <summary>
    /// Variable que almacena la lecutura de la interpolacion al caminar
    /// </summary>
    private readonly float m_interpolation = 10;

    /// <summary>
    /// Variable que almacena vector por donde nos hemos movido
    /// </summary>
    [SerializeField]
    private Vector3 m_currentDirection = Vector3.zero;
    /// <summary>
    /// Variable que almacena el tiempo de caida del salto
    /// </summary>
    [SerializeField]
    private float m_jumpTimeStamp = 0;
    /// <summary>
    /// Variable que almacena la variacion minima del intervalo de salto
    /// </summary>
    [SerializeField]
    private float m_minJumpInterval = 0.25f;
    /// <summary>
    /// Variable que almacena bool de si se toca el iput de salto   
    /// </summary>
    [SerializeField]
    private bool m_jumpInput = false;
    /// <summary>
    /// Variable que almacena bool de si el player esta tocando el suelo
    /// </summary>
    [SerializeField]
    private bool m_isGrounded;
    /// <summary>
    /// Variable que almacena bool de si el player esta en el aire
    /// </summary>
    private bool m_wasGrounded;
    /// <summary>
    /// Listado que almacena objetos donde toco suelo
    /// </summary>
    [SerializeField]
    private List<Collider> m_collisions = new List<Collider>();

    [SerializeField]
    private float m_turnSpeed;
    public float speeIsInteracting;

    private bool comboFlag;

    private void OnCollisionEnter(Collision collision)
    {
        #region Contacto entrada con el suelo

        //contacto donde ocurre la entrada de la colisión.
        ContactPoint[] contactPoints = collision.contacts;

        // for para validar todos los contactos
        for (int i = 0; i < contactPoints.Length; i++)
        {
            //validacion de si los vectores de colision son perpendiculares al player.
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                //validacion de que no exista anteriormente esta colision
                if (!m_collisions.Contains(collision.collider))
                {
                    // agregar colision 
                    m_collisions.Add(collision.collider);
                }

                // esta tocando el piso 
                m_isGrounded = true;
            }
        }

        #endregion
    }

    private void OnCollisionStay(Collision collision)
    {
        #region Contacto con el suelo

        //contacto donde ocurre la colisión.
        ContactPoint[] contactPoints = collision.contacts;

        //validacion de si toca superficie normal
        bool validSurfaceNormal = false;

        // for para validar todos los contactos
        for (int i = 0; i < contactPoints.Length; i++)
        {
            //validacion de si los vectores de colision son perpendiculares al player.
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        // validacion de si aun sigue en el piso 
        if (validSurfaceNormal)
        {
            //sigue en el piso
            m_isGrounded = true;

            //validacion de que no exista anteriormente esta colision
            if (!m_collisions.Contains(collision.collider))
            {
                // agregar colision 
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            //validacion de que no exista anteriormente esta colision
            if (m_collisions.Contains(collision.collider))
            {
                // quitar colision 
                m_collisions.Remove(collision.collider);
            }

            //validar si no hay ningun contacto
            if (m_collisions.Count == 0)
            {
                //ya no esta en el piso
                m_isGrounded = false;
            }
        }

        #endregion
    }

    private void OnCollisionExit(Collision collision)
    {
        #region Contacto salida con el suelo

        //validacion de que ya no sigue la anterior colision
        if (m_collisions.Contains(collision.collider))
        {
            //quitar la anterior colision
            m_collisions.Remove(collision.collider);
        }

        //validar si no hay ningun contacto
        if (m_collisions.Count == 0)
        {
            //ya no esta en el piso
            m_isGrounded = false;
        }

        #endregion
    }

    public void HandleActions()
    {
        if (!Player.Instance.isActive)
            return;

        #region Control de Salto

        // envio a controlador de animacion del bool si toca el piso 
        Player.Instance.animationController.m_animator.SetBool("Grounded", m_isGrounded);
        Debug.Log("isgrounded" + m_isGrounded);

        //Actualizacion de bool de aire
        m_wasGrounded = m_isGrounded;

        // ajuste de variable de input a falso 
        m_jumpInput = false;

        if (Input.GetButtonDown("Jump") && m_isGrounded &&
            !Player.Instance.isInteracting)
        {
            m_jumpInput = true;
        }

        #endregion

        #region Control de Attackes

        if (Input.GetButtonDown("Fire1"))
        {

            if (!m_isGrounded && !Player.Instance.attackAir)
            {
                Player.Instance.animationController.m_animator.SetBool("AttackAir", true);
                Player.Instance.animationController.m_animator.SetInteger("AttackIndex", 0);
                Player.Instance.animationController.m_animator.SetTrigger("Attack");
                return;
            }

            if (Player.Instance.canDoCombo)
            {
                comboFlag = true;
                AttackCombo();
                comboFlag = false;
            }
            else
            {
                if (Player.Instance.animationController.m_animator.GetBool("IsInteracting") || 
                    Player.Instance.attackAir || Player.Instance.canDoCombo) return;

                Player.Instance.animationController.m_animator.SetBool("IsInteracting", true);
                Player.Instance.animationController.m_animator.SetInteger("AttackIndex", 1);
                Player.Instance.animationController.m_animator.SetTrigger("Attack");
            }
        }

        #endregion

        #region Control de Sigilo

        if (Input.GetKey(KeyCode.Tab))
        {
            Debug.Log("XD");
            ChangeVelocity(!Player.Instance.isStealth);
        }
        else
        {
            ChangeVelocity(Player.Instance.isStealth);
        }

        #endregion
    }

    void AttackCombo()
    {
        if (comboFlag)
        {
            Player.Instance.animationController.m_animator.SetBool("CanDoCombo", false);

            if (Player.Instance.animationController.m_animator.GetInteger("AttackIndex") == 1)
            {
                Player.Instance.animationController.m_animator.SetInteger("AttackIndex", 2);
                Player.Instance.animationController.m_animator.SetTrigger("Attack");
                return;
            }

            if (Player.Instance.animationController.m_animator.GetInteger("AttackIndex") == 2)
            {
                if (!Player.Instance.canDoCombo)
                    return;
                Player.Instance.animationController.m_animator.SetInteger("AttackIndex", 3);
                Player.Instance.animationController.m_animator.SetTrigger("Attack");
                return;
            }
        }
    }

    public void ChangeVelocity(bool stealthState)
    {
        if (!stealthState)
        {
            moveSpeed = 12;
            Player.Instance.isStealth = false;
        }
        else
        {
            moveSpeed = 5;
            Player.Instance.isStealth = true;
        }
    }


    /// <summary>
    /// Funcion encargada de activar punto de daño en attack
    /// </summary>
    public void ChangeAttackPointState()
    {

    }

    /// <summary>
    /// Funcion encargada de calcular y aplicar nueva direccion al jugador
    /// </summary>
    public void DirectUpdate()
    {
        if (!Player.Instance.isInteracting 
            && Player.Instance.isActive) 
        {
            //Lectura de input vertical
            float v = Input.GetAxis("Vertical");
            //Lectura de input horizontal
            float h = Input.GetAxis("Horizontal");

            // Interpolacion en la variacion  de la velocidad capatada por el input 
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);
        }
        else
        {
            m_currentV = m_currentH = 0;
        }

        // aplicar movimiento apartir de la camara 
        Vector3 direction = Camera.main.transform.forward * m_currentV + Camera.main.transform.right * m_currentH;

        float directionLength = direction.magnitude;
        
        direction.y = 0;
        
        direction = direction.normalized * directionLength;

        //si se mueve 
        if (direction != Vector3.zero)
        {
            //Interpolacion del giro 
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            //aplicar rotacion al player
            transform.rotation = Quaternion.LookRotation(m_currentDirection * m_turnSpeed * Time.deltaTime);

            //aplicar movimiento al player
            transform.position += m_currentDirection * moveSpeed * Time.deltaTime;
        }
        
        //envio de velocidad del movimiento a controlador de animaciones
        Player.Instance.animationController.m_animator.SetFloat("MoveSpeed", direction.magnitude);

        // llamada del salto y caida 
        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        //bool que valida si el salto fue afectuado para inicial conteo de caida
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        //validacion despues de precionar input de salto
        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            //asignacion de tiempo de caida
            m_jumpTimeStamp = Time.time;

            //agregar fuerza de salto
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        // validacion de que esta en el aire 
        if (!m_wasGrounded && m_isGrounded)
        {
            // activar trigger de caida
            Player.Instance.animationController.m_animator.SetTrigger("Land");
        }

        // validacion de que despego del suelo
        if (!m_isGrounded && m_wasGrounded)
        {
            // activar trigger de salto
            Player.Instance.animationController.m_animator.SetTrigger("Jump");
        }
    }
}

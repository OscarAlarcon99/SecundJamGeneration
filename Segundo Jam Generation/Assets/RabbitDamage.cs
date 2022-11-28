using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDamage : MonoBehaviour
{
    [SerializeField] Rabbit brain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttackPoint"))
        {
            brain.Die();
        }
    }
}

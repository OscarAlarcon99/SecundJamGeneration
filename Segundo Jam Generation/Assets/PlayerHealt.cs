using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public float life;
    
    // Start is called before the first frame update
    void Start()
    {
        life = 100;    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !Player.Instance.isInvulnerable)
        {
            Player.Instance.isInvulnerable = true;
            life -= 20;
            StartCoroutine(Player.Instance.DamagePlayer());
        }
    }
}

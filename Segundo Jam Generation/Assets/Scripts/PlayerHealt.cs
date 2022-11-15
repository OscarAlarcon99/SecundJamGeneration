using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public float healt;
    
    // Start is called before the first frame update
    void Start()
    {
        healt = 100;    
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Player.Instance.isInvulnerable = true;
            healt -= 20;
            StartCoroutine(Player.Instance.DamagePlayer());
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !Player.Instance.isInvulnerable)
        {
            Player.Instance.isInvulnerable = true;
            healt -= 20;
            StartCoroutine(Player.Instance.DamagePlayer());
        }
    }
}

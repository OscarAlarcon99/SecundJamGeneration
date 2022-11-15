using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveDamage : MonoBehaviour
{
    public EnemyController Enemy;
    int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PlayerAttack");
        if (other.CompareTag("PlayerAttack"))
        {   
            
            health = health - 1;
            Debug.Log(health);
            Enemy.Die(health);
        }
    }
}

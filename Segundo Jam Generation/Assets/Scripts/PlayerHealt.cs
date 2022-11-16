using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public float healt;
    private int currentTime;
    public float timeHungry;
    // Start is called before the first frame update
    void Start()
    {
        healt = 100;
        StartCoroutine(Hunger()); 
    }
   
    public IEnumerator Hunger()
    {
        while (healt >= 0)
        {
            yield return new WaitForSecondsRealtime(timeHungry);
            healt -= Time.deltaTime;
            Player.Instance.vFXController.SettupEffectHealth();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (!Player.Instance.isActive)
            return;

        if (other.CompareTag("Winner"))
        {
            StartCoroutine(GameManager.Instance.Winner());
        }

        if (other.CompareTag("enemyAttackPoint") && !Player.Instance.isInvulnerable)
        {
            SoundManager.Instance.PlayNewSound("DamagePlayer");
            Player.Instance.isInvulnerable = true;
            healt -= 15;
            StartCoroutine(Player.Instance.DamagePlayer());
        }
    }
}

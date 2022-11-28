using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public float timeStart;
    public float healt;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        healt = 100;
        StartCoroutine(CountDownLife());
    }

    public IEnumerator CountDownLife()
    {
        yield return new WaitForSeconds(timeStart);
        Debug.Log("GettingDamage");
        Player.Instance.healtController.healt -= damage;
        RestartCountDown();
    }

    public void RestartCountDown()
    {
        StartCoroutine(CountDownLife());
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
            healt -= 20;
            StartCoroutine(Player.Instance.DamagePlayer());
        }
    }
}

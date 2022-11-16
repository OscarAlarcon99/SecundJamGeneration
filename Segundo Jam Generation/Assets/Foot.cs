using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public float time;
    public float finalTime;
    public float lifeExtra;

    void Update()
    {
        if (Player.Instance.moveController.canEat && Player.Instance.isEating)
        {
            time += Time.deltaTime;

            if (time >= finalTime)
            {
                Player.Instance.GetFoot();
                time = 0;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && this.enabled)
        {
            Player.Instance.moveController.canEat = true;
        }
    }
}

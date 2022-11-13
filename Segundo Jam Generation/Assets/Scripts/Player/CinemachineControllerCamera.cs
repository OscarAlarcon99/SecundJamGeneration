using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineControllerCamera : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook freeAim;

    public float lookspeed;
    public void InputCamera()
    {
        if (!Player.Instance.IsActive)
            return;
    }

}

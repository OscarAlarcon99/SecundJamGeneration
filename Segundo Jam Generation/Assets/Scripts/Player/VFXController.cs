using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VFXController : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera Camera;
    public GameObject[] postProcessings;
    public Volume volume;
    public ChromaticAberration chromaticAberration;
    public Vignette vignette;
    int index;

    private void Start()
    {
        SettupEffectHealth();
    }
    
    public void SettupEffectHealth()
    {
        volume = postProcessings[index].GetComponentInChildren<Volume>();
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGet<Vignette>(out vignette);
        ChangeEffectHealth();
    }

    public void ChangeEffectHealth()
    {
        if (Player.Instance.healtController.healt < 100 && Player.Instance.healtController.healt > 90)
        {
            vignette.intensity.value = 0;
            chromaticAberration.intensity.value = 0;
        }
        if (Player.Instance.healtController.healt > 75 && Player.Instance.healtController.healt > 90)
        {
            vignette.intensity.value = 0.2f;
            chromaticAberration.intensity.value = 0.2f;
        }

        if (Player.Instance.healtController.healt < 75 && Player.Instance.healtController.healt > 50)
        {
            vignette.intensity.value = 0.3f;
            chromaticAberration.intensity.value = 0.3f; 
        }
        
        if (Player.Instance.healtController.healt < 50 && Player.Instance.healtController.healt > 25)
        {
            vignette.intensity.value = 0.4f;
            chromaticAberration.intensity.value = 0.75f;
        }
        
        if (Player.Instance.healtController.healt < 25)
        {
            vignette.intensity.value = 0.5f;
            chromaticAberration.intensity.value = 1f;
        }
    }
    public void ChangePostProccesing()
    {
        if (index == postProcessings.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        
        for (int i = 0; i < postProcessings.Length; i++)
        {
            if (i == index)
            {
                postProcessings[i].gameObject.SetActive(true);
                SettupEffectHealth();
            }
            else
            {
                postProcessings[i].gameObject.SetActive(false);
            }
        }

    }
}

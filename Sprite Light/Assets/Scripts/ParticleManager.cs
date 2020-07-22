using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ParticleManager : MonoBehaviour
{
    public Light2D Light;

    public void SetParticles(bool val)
    {
        if (val)
        {
            AddParticles();
        }
        else
        {
            RemoveParticles();
        }
    }

    void AddParticles()
    {
        var newPS = Instantiate(AddLightUI.Instance.ParticleRegistry[ParticleStyle.Sparkler], Light.transform);
        newPS.transform.localPosition = Vector3.zero;
        var main = newPS.GetComponent<ParticleSystem>().main;
        main.startColor = Light.color;
    }

    void RemoveParticles()
    {
        foreach (var ps in Light.GetComponentsInChildren<ParticleSystem>())
        {
            Destroy(ps.gameObject);
        }
    }
}

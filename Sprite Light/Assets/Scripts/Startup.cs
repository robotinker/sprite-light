using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [Range(0f, 1f)]
    public float GlobalIllumination = 0.5f;

    public List<LightData> StartingLights;

    void Start()
    {
        AddLightUI.Instance.GlobalIllumination.StealthSet(GlobalIllumination);

        foreach (var lightData in StartingLights)
        {
            ActiveLightTracker.Instance.CreateLight(lightData);
        }
        ActiveLightTracker.Instance.SetNoActiveLight();
    }
}

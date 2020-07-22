using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ActiveLightTracker : MonoBehaviour
{
    public Action OnLightSelected;
    public Action OnNoLightSelected;

    public GameObject LightPrefab;
    public Light2D ActiveLight { get; private set; }
    public GameObject TrackerPrefab;
    GameObject Tracker;

    public static ActiveLightTracker Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnNoLightSelected?.Invoke();
    }

    public void SetActiveLight(GameObject newObj)
    {
        if (ActiveLight == newObj)
            return;

        if (Tracker == null)
        {
            Tracker = Instantiate(TrackerPrefab, Camera.main.WorldToScreenPoint(newObj.transform.position), Quaternion.identity);
            Tracker.transform.SetParent(transform);
        }

        ActiveLight = newObj.GetComponentInChildren<Light2D>();

        OnLightSelected?.Invoke();
    }

    public void SetNoActiveLight()
    {
        if (ActiveLight == null)
            return;

        if (Tracker != null)
        {
            Destroy(Tracker);
        }

        ActiveLight = null;

        OnNoLightSelected?.Invoke();
    }

    public void CreateLight()
    {
        CreateLight(null);
    }

    public void CreateLight(LightData data = null)
    {
        var newLight = Instantiate(LightPrefab);

        if (data != null)
        {
            var light = newLight.GetComponentInChildren<Light2D>();
            light.color = data.Color;
            light.intensity = data.Intensity;
            light.GetComponentInParent<FlickerManager>().Apply(data.Flicker);
            light.GetComponentInParent<PathingManager>().SetPathing(data.Wander ? PathingMode.Wander : PathingMode.Static);
            light.GetComponentInParent<ParticleManager>().SetParticles(data.Particles);
        }

        SetActiveLight(newLight);
        AddLightUI.Instance.UpdateUI(ActiveLight);
    }

    public void DestroyActiveLight()
    {
        if (ActiveLight != null)
        {
            Destroy(ActiveLight.GetComponentInParent<FlickerManager>().gameObject);
            SetNoActiveLight();
        }
    }

    private void Update()
    {
        if (ActiveLight != null)
        {
            Tracker.transform.position = Camera.main.WorldToScreenPoint(ActiveLight.transform.position);
        }
    }
}

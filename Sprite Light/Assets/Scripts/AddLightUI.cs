using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class AddLightUI : MonoBehaviour
{
    public Animator MyAnimator;
    public SliderValue Intensity;
    public SliderValue LightColorR;
    public SliderValue LightColorG;
    public SliderValue LightColorB;
    public Toggle FlickerToggle;
    public Toggle ParticleToggle;
    public Toggle WanderToggle;

    public SliderValue GlobalIllumination;

    public Light2D GlobalLight;

    public List<ParticleKVP> ParticleKVPEntries;
    Dictionary<ParticleStyle, GameObject> ParticleRegistry = new Dictionary<ParticleStyle, GameObject>();

    const string AnimShowParam = "Show";

    Light2D ActiveLight { get { return ActiveLightTracker.Instance.ActiveLight; } }

    public static AddLightUI Instance;

    private void Awake()
    {
        Instance = this;
        foreach (var particleKVP in ParticleKVPEntries)
        {
            ParticleRegistry[particleKVP.Style] = particleKVP.Prefab;
        }
    }

    private void Start()
    {
        GlobalIllumination.OnChange += x => GlobalLight.intensity = x;
        Intensity.OnChange += x => { if (ActiveLight != null) ActiveLight.intensity = x; };
        LightColorR.OnChange += x => { if (ActiveLight != null) ActiveLight.color = new Color(x, ActiveLight.color.g, ActiveLight.color.b); };
        LightColorG.OnChange += x => { if (ActiveLight != null) ActiveLight.color = new Color(ActiveLight.color.r, x, ActiveLight.color.b); };
        LightColorB.OnChange += x => { if (ActiveLight != null) ActiveLight.color = new Color(ActiveLight.color.r, ActiveLight.color.g, x); };

        Intensity.Initialize();
        LightColorR.Initialize();
        LightColorG.Initialize();
        LightColorB.Initialize();
        GlobalIllumination.Initialize();

        FlickerToggle.onValueChanged.AddListener(x => { if (ActiveLight != null) ActiveLight.GetComponentInParent<FlickerManager>().Apply(x); });
        WanderToggle.onValueChanged.AddListener(x => { if (ActiveLight != null) ActiveLight.GetComponentInParent<PathingManager>().SetPathing(x ? PathingMode.Wander : PathingMode.Static); });
        ParticleToggle.onValueChanged.AddListener(x => { if (ActiveLight != null) SetParticles(ActiveLight, x); });
    }

    public void SetShowing(bool val)
    {
        MyAnimator.SetBool(AnimShowParam, val);
    }

    public void UpdateUI(Light2D light)
    {
        Intensity.StealthSet(light.intensity);
        LightColorR.StealthSet(light.color.r);
        LightColorG.StealthSet(light.color.g);
        LightColorB.StealthSet(light.color.b);

        WanderToggle.isOn = light.GetComponentInParent<PathingManager>().Mode == PathingMode.Wander;
        FlickerToggle.isOn = light.GetComponentInParent<FlickerManager>().IsFlickering;
        ParticleToggle.isOn = light.GetComponentInChildren<ParticleSystem>() != null;
    }

    void SetParticles(Light2D light, bool val)
    {
        if (val)
        {
            AddParticles(light);
        }
        else
        {
            RemoveParticles(light);
        }
    }

    void AddParticles(Light2D light)
    {
        var newPS = Instantiate(ParticleRegistry[ParticleStyle.Sparkler], light.transform);
        newPS.transform.localPosition = Vector3.zero;
        var main = newPS.GetComponent<ParticleSystem>().main;
        main.startColor = light.color;
    }

    void RemoveParticles(Light2D light)
    {
        foreach (var ps in light.GetComponentsInChildren<ParticleSystem>())
        {
            Destroy(ps.gameObject);
        }
    }
}

public enum PathingMode
{
    Static,
    Wander
}

public enum ParticleStyle
{
    None,
    Sparkler
}

[Serializable]
public class ParticleKVP
{
    public ParticleStyle Style;
    public GameObject Prefab;
}

[Serializable]
public class SliderValue
{
    public Slider Slider;
    public float Value;
    public float Default;
    public float Min;
    public float Max;

    public Action<float> OnChange;

    public void Initialize()
    {
        Slider.onValueChanged.AddListener(x => Set(x));
        StealthSet(Default);
    }

    public float Get()
    {
        return (Value - Min) / (Max - Min);
    }

    public void Set(float value)
    {
        Value = Min + value * (Max - Min);
        OnChange?.Invoke(Value);
    }

    public void StealthSet(float value)
    {
        Value = value;
        Slider.value = Get();
    }
}
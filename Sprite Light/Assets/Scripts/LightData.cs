using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LightData
{
    public float Intensity = 1;
    public Color Color = Color.white;
    public bool Flicker = true;
    public bool Particles = true;
    public bool Wander = true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerManager : MonoBehaviour
{
    public Animator FlickerAnimator;

    public bool IsFlickering { get; private set; }

    private void Awake()
    {
        IsFlickering = FlickerAnimator.enabled;
    }

    public void Apply(bool isFlickering)
    {
        IsFlickering = isFlickering;
        FlickerAnimator.enabled = isFlickering;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingManager : MonoBehaviour
{
    public Animator PositionAnimator;

    public PathingMode Mode { get; private set; }

    private void Awake()
    {
        Mode = PositionAnimator.enabled ? PathingMode.Wander : PathingMode.Static;
    }

    public void SetPathing(PathingMode mode)
    {
        Mode = mode;
        switch (mode)
        {
            case PathingMode.Static:
                PositionAnimator.enabled = false;
                break;
            case PathingMode.Wander:
                PositionAnimator.enabled = true;
                PositionAnimator.speed = Random.Range(0.8f, 1.2f);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public Transform LightTransform;
    public Animator LightPositionAnimator;

    bool IsControllingByMouse;
    const float AnimationResetTime = 2f;
    float AnimationResetTimer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsControllingByMouse = true;
            LightPositionAnimator.enabled = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsControllingByMouse = false;
            AnimationResetTimer = AnimationResetTime;
        }

        if (AnimationResetTimer > 0f)
        {
            AnimationResetTimer -= Time.deltaTime;
            if (AnimationResetTimer <= 0f)
            {
                LightPositionAnimator.enabled = true;
            }
        }

        if (IsControllingByMouse)
        {
            LightTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LightTransform.Translate(Vector3.back * LightTransform.position.z);
        }
    }
}

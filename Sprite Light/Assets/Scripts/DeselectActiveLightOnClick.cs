using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectActiveLightOnClick : MonoBehaviour
{
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            AddLightUI.Instance.SetShowing(false);
            ActiveLightTracker.Instance.SetNoActiveLight();
        }
    }
}
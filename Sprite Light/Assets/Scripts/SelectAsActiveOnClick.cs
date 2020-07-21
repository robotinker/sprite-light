using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectAsActiveOnClick : MonoBehaviour
{
    bool IsFollowingMouse;

    private void Update()
    {
        if (IsFollowingMouse)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += Vector3.back * transform.position.z;
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ActiveLightTracker.Instance.SetActiveLight(gameObject);
            AddLightUI.Instance.SetShowing(true);
            AddLightUI.Instance.UpdateUI(ActiveLightTracker.Instance.ActiveLight);
            AddLightUI.Instance.WanderToggle.isOn = false;
            IsFollowingMouse = true;
        }
    }

    private void OnMouseUp()
    {
        IsFollowingMouse = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    void Start()
    {
        ActiveLightTracker.Instance.CreateLight();
        ActiveLightTracker.Instance.SetNoActiveLight();
    }
}

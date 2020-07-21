using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageSwitcher : MonoBehaviour
{
    Image MyImage;

    public Sprite LightSelectedSprite;
    public Sprite NoLightSelectedSprite;
    
    void Awake()
    {
        MyImage = GetComponent<Image>();
    }

    private void Start()
    {
        ActiveLightTracker.Instance.OnLightSelected += HandleNewActiveLight;
        ActiveLightTracker.Instance.OnNoLightSelected += HandleNoActiveLight;
    }

    void HandleNewActiveLight()
    {
        MyImage.sprite = LightSelectedSprite;
    }

    void HandleNoActiveLight()
    {
        MyImage.sprite = NoLightSelectedSprite;
    }
}

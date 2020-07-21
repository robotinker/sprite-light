using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Animator MyAnimator;
    const string AnimShowParam = "Show";

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MyAnimator.SetBool(AnimShowParam, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MyAnimator.SetBool(AnimShowParam, true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MyAnimator.SetBool(AnimShowParam, true);
    }

    IEnumerator DismissButton()
    {
        yield return new WaitForSeconds(3f);
        MyAnimator.SetBool(AnimShowParam, false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
public class ButtonsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //dealing with button tweening and stuff.
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float scaleMult = 1.2f;
    [SerializeField] private GameObject skullImage;
    [SerializeField] private GameObject potionImage; 



    public void EnlargeButton()
    {
        //Debug.Log("Hover ON");
        skullImage.SetActive(true);
        potionImage.SetActive(true);
        this.transform.DOScale(scaleMult, fadeTime).SetEase(Ease.OutBounce); 
    }

    public void ReduceButton()
    {
        //Debug.Log("Hover OFF");
        skullImage.SetActive(false);
        potionImage.SetActive(false);
        this.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnlargeButton();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ReduceButton();
    }

    IEnumerator WaitOneFrame()
    {
        yield return 0; 
    }
    public void OnDisable()
    {
        StartCoroutine(WaitOneFrame());
        ReduceButton();
        StartCoroutine(WaitOneFrame()); 
    }
}

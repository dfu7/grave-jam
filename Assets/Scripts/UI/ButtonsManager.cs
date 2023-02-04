using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
public class ButtonsManager : MonoBehaviour
{
    //dealing with button tweening and stuff.
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float scaleMult = 1.5f;

    public void EnlargeButton()
    {
        Debug.Log("Hover ON");
        this.transform.DOScale(scaleMult, fadeTime).SetEase(Ease.OutBounce); 
    }

    public void ReduceButton()
    {
        Debug.Log("Hover OFF");
        this.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce); 
    }
    
    

}

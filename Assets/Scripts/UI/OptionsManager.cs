using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    //deals with opening/closing options menu and volume stuff
    //Idk i dont wanna make 2 separate scripts for those
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject howToPanel;
    

    public void SetMasterVolume(float vol)
    {
        audioMixer.SetFloat("masterVol", vol); 
    }

    public void SetBGMVolume(float vol)
    {
        audioMixer.SetFloat("bgmVol", vol); 
    }
    
    public void SetSFXVolume(float vol)
    {
        audioMixer.SetFloat("sfxVol", vol); 
    }
    
    public void OpenOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    
    public void CloseOptions()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void OpenHowTo()
    {
        mainPanel.SetActive(false);
        howToPanel.SetActive(true);
    }
    
    public void CloseHowTo()
    {
        mainPanel.SetActive(true);
        howToPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    //deals with opening/closing options menu and volume stuff
    //Idk i dont wanna make 2 separate scripts for those
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject mainPanel; 

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

    public void OpenSubMenu(GameObject submenu)
    {
        mainPanel.SetActive(false);
        submenu.SetActive(true);
    }
    
    public void CloseSubMenu(GameObject submenu)
    {
        mainPanel.SetActive(true);
        submenu.SetActive(false);
    }
}

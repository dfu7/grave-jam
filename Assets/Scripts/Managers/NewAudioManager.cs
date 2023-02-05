using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;

    [SerializeField] private AudioClip MainMenuMusic;
    [SerializeField] private AudioClip LevelMusic;
    [SerializeField] private AudioClip ButtonConfirm;
    [SerializeField] private AudioClip ButtonCancel;

    [HideInInspector] public bool Wait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMenuTheme()
    {
        audioSourceBGM.clip = MainMenuMusic;
        audioSourceBGM.Play();
    }

    public void PlayLevelMusic()
    {
        audioSourceBGM.clip = LevelMusic;
        audioSourceBGM.Play();
    }

    public void PlayButtonConfirm()
    {
        audioSourceSFX.clip = ButtonConfirm;
        audioSourceSFX.Play();
    }
    public void PlayButtonCancel()
    {
        audioSourceSFX.clip = ButtonCancel;
        audioSourceSFX.Play();
    }
}

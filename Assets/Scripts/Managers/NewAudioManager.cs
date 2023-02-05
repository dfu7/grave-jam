using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    public AudioSource audioSourceSFX;

    [SerializeField] private AudioClip ButtonConfirm;
    [SerializeField] private AudioClip ButtonCancel;
    [SerializeField] private AudioClip ScrollSound;
    [SerializeField] private AudioClip StartGame;

    public float TimeToWait = 0.55f;

    [HideInInspector] public bool Wait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void PlayScrollSound()
    {
        audioSourceSFX.clip = ScrollSound;
        audioSourceSFX.Play();
    }

    public void PlayButtonStartGame()
    {
        audioSourceSFX.clip = StartGame;
        audioSourceSFX.Play();
    }
}

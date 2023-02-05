using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioManager : MonoBehaviour
{
    public AudioSource audioSourceSFX;
    public AudioSource longAudioSourceSFX;

    [SerializeField] private AudioClip ButtonConfirm;
    [SerializeField] private AudioClip ButtonCancel;
    [SerializeField] private AudioClip ScrollSound;
    [SerializeField] private AudioClip StartGame;
    [SerializeField] private AudioClip StartCountdown;
    [SerializeField] private AudioClip EndWhistle;
    [SerializeField] private AudioClip StartMoving;
    [SerializeField] private AudioClip GraveRising;
    [SerializeField] private AudioClip Coin;
    [SerializeField] private AudioClip Ghost;
    [SerializeField] private AudioClip Stun;

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

    public void PlayStartCountdown()
    {
        longAudioSourceSFX.clip = StartCountdown;
        longAudioSourceSFX.Play();
    }
    public void PlayEndWhistle()
    {
        longAudioSourceSFX.clip = EndWhistle;
        longAudioSourceSFX.Play();
    }

    public void PlayStartMoving()
    {
        audioSourceSFX.clip = StartMoving;
        audioSourceSFX.Play();
    }

    public void PlayGraveRising()
    {
        audioSourceSFX.clip = GraveRising;
        audioSourceSFX.Play();
    }

    public void PlayCoin()
    {
        audioSourceSFX.clip = Coin;
        audioSourceSFX.Play();
    }

    public void PlayGhost()
    {
        audioSourceSFX.clip = Ghost;
        audioSourceSFX.Play();
    }

    public void PlayStun()
    {
        longAudioSourceSFX.clip = Stun;
        longAudioSourceSFX.Play();
    }
}

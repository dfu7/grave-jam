using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NewAudioManager : MonoBehaviourPunCallbacks
{
    public AudioSource audioSourceSFX;
    public AudioSource longAudioSourceSFX;

    public AudioSource otherAudioSourceSFX;
    public AudioSource otherLongAudioSourceSFX;

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

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
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
        view.RPC("RPC_PlayButtonStartGame", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayButtonStartGame()
    {
        otherAudioSourceSFX.clip = StartGame;
        otherAudioSourceSFX.Play();
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
        view.RPC("RPC_PlayStartMoving", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayStartMoving()
    {
        otherAudioSourceSFX.clip = StartMoving;
        otherAudioSourceSFX.Play();
    }

    public void PlayGraveRising()
    {
        audioSourceSFX.clip = GraveRising;
        audioSourceSFX.Play();
        view.RPC("RPC_PlayGraveRising", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayGraveRising()
    {
        otherAudioSourceSFX.clip = GraveRising;
        otherAudioSourceSFX.Play();
    }

    public void PlayCoin()
    {
        audioSourceSFX.clip = Coin;
        audioSourceSFX.Play();
        view.RPC("RPC_PlayCoin", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayCoin()
    {
        otherAudioSourceSFX.clip = Coin;
        otherAudioSourceSFX.Play();
    }

    public void PlayGhost()
    {
        audioSourceSFX.clip = Ghost;
        audioSourceSFX.Play();
        view.RPC("RPC_PlayGhost", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayGhost()
    {
        otherAudioSourceSFX.clip = Ghost;
        otherAudioSourceSFX.Play();
    }

    public void PlayStun()
    {
        longAudioSourceSFX.clip = Stun;
        longAudioSourceSFX.Play();
        view.RPC("RPC_PlayStun", RpcTarget.Others);
    }

    [PunRPC]
    public void RPC_PlayStun()
    {
        otherLongAudioSourceSFX.clip = Stun;
        otherLongAudioSourceSFX.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class VictoryManager : MonoBehaviourPunCallbacks
{
    [HideInInspector] public PlayerController MasterObject;
    [HideInInspector] public PlayerController PlayerObject;

    [SerializeField] private NewAudioManager newAudioManager;

    private TombstoneSpawner tombstoneSpawner;
    private int NumberToWin;

    [SerializeField] private string MainMenuName;
    [SerializeField] private GameObject VictoryScreen;
    [SerializeField] private GameObject LossScreen;

    private bool EndGameCalled = false;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        VictoryScreen.SetActive(false);
        LossScreen.SetActive(false);

        tombstoneSpawner = FindObjectOfType<TombstoneSpawner>();
        NumberToWin = tombstoneSpawner.numOfGoodTombstones / 2;

        //accounts for int division truncation
        if (tombstoneSpawner.numOfGoodTombstones % 2 == 1)
            NumberToWin++;
    }

    // Update is called once per frame
    void Update()
    {
        if(!EndGameCalled && MasterObject != null && PlayerObject != null)
        {
            if (MasterObject.Score >= NumberToWin)
            {
                EndGame(true);
            }
            else if (PlayerObject.Score >= NumberToWin)
            {
                EndGame(false);
            }
        }

        //THIS IS FOR DEBUG
        /*if (MasterObject != null && PlayerObject != null)
            EndGame(true);
        */
    }

    public void EndGame(bool MasterWon)
    {
        Debug.LogError("In EndGame");
        if(MasterWon)
        {
            //Master won and I am Master
            if(PhotonNetwork.IsMasterClient)
            {
                VictoryScreen.SetActive(true);
            }
            else
            {
                LossScreen.SetActive(true);
            }
        }
        else
        {
            //Master won and I am not Master
            if(PhotonNetwork.IsMasterClient)
            {
                LossScreen.SetActive(true);
            }
            else
            {
                VictoryScreen.SetActive(true);
            }
        }
        EndGameCalled = true;
    }

    public void OnClickExit()
    {
        newAudioManager.PlayButtonConfirm();
        StartCoroutine(ExitClicked());
    }

    public IEnumerator ExitClicked()
    {
        yield return new WaitForSeconds(newAudioManager.TimeToWait);
        AudioManager.instance.PlayMenuMusic();
        SceneManager.LoadScene(MainMenuName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class VictoryManager : MonoBehaviourPunCallbacks
{
    public PlayerController MasterObject;
    public PlayerController PlayerObject;

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
            EndGameCalled = true;
        }

        //THIS IS FOR DEBUG
        /*if (MasterObject != null && PlayerObject != null)
            EndGame(true);
        */
    }

    public void EndGame(bool MasterWon)
    {
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
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(MainMenuName);
    }
}

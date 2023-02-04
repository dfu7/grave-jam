using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VictoryManager : MonoBehaviourPunCallbacks
{
    private GameObject MasterObject;
    private GameObject PlayerObject;
    private GameObject[] playerObjects;

    private TombstoneSpawner tombstoneSpawner;
    private int NumberToWin;



    // Start is called before the first frame update
    void Start()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        //if (playerObjects[0].GetPhotonView().)
        tombstoneSpawner = FindObjectOfType<TombstoneSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if
    }
}

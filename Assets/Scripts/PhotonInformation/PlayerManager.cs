using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;

//Written by Ed
public class PlayerManager : MonoBehaviourPunCallbacks//, IPunObservable
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private string PrefabName;
    //[SerializeField] private string MasterTag;
    //[SerializeField] private string PlayerTag;

    private PhotonView view;

    //private GameStateManager.GAMESTATE LastState = GameStateManager.GAMESTATE.PLAYING;    

    //private bool CinematicsFix;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        //CinematicsFix = false;

        if (view.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        //TODO: SPAWN SHIT
        
        bool Master = PhotonNetwork.IsMasterClient;
        GameObject InstantiatedPlayer = SpawnManager.Instance.OnlineSpawn(Master, PrefabName);
        Debug.Log("Spawned in CreateController");
    }
}

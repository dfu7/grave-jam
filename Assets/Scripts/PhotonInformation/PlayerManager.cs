using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;

//Written by Ed
public class PlayerManager : MonoBehaviourPunCallbacks//, IPunObservable
{
    //[SerializeField] private GameObject Prefab;
    //[SerializeField] private string PrefabName;
    //[SerializeField] private string MasterTag;
    //[SerializeField] private string PlayerTag;

    private PhotonView view;

    [SerializeField] private string MasterPrefab;
    [SerializeField] private string PlayerPrefab;

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
        if(Master)
        {
            GameObject InstantiatedPlayer = SpawnManager.Instance.OnlineSpawn(Master, MasterPrefab);
            InstantiatedPlayer.GetComponent<PlayerController>().canMove = false;
            //Debug.LogError("Spawned in CreateController at " + InstantiatedPlayer.transform.position);
        }
        else
        {
            GameObject InstantiatedPlayer = SpawnManager.Instance.OnlineSpawn(Master, PlayerPrefab);
            InstantiatedPlayer.GetComponent<PlayerController>().canMove = false;
            //Debug.LogError("Spawned in CreateController at " + InstantiatedPlayer.transform.position);
        }
    }
}

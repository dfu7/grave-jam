using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{

    public static SpawnManager Instance;

    private static GameObject[] SpawnPoints;// = new Transform[2];

    private PlayerController[] playerObjects;
    private PlayerController MasterObject;
    private PlayerController PlayerObject;

    [SerializeField] private VictoryManager victoryManager;
    [SerializeField] private GameObject LoadingScreen;

    private bool ObjectsSet;

    private PhotonView view;

    //set the static variables
    private void Awake()
    {
        Instance = new SpawnManager();

        SpawnPoints = new GameObject[2];
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        playerObjects = FindObjectsOfType<PlayerController>();

        LoadingScreen.SetActive(true);

        ObjectsSet = false;

        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (playerObjects != null && playerObjects.Length == 2)
        {
            if (view.ViewID % PhotonNetwork.MAX_VIEW_IDS == playerObjects[0].gameObject.GetPhotonView().ViewID % PhotonNetwork.MAX_VIEW_IDS)
            {
                //found "my" player as the master
                if (PhotonNetwork.IsMasterClient)
                {
                    MasterObject = playerObjects[0];
                    PlayerObject = playerObjects[1];
                }
                else
                {
                    MasterObject = playerObjects[1];
                    PlayerObject = playerObjects[0];
                }
            }
            else
            {
                //found "not my" player as the master
                if (PhotonNetwork.IsMasterClient)
                {
                    MasterObject = playerObjects[1];
                    PlayerObject = playerObjects[0];
                }
                else
                {
                    MasterObject = playerObjects[0];
                    PlayerObject = playerObjects[1];
                }
            }
            BothIn();
        }
        else
        {
            playerObjects = FindObjectsOfType<PlayerController>();
            Debug.Log("lookingstill");
        }
    }

    public void BothIn()
    {
        ObjectsSet = true;
        MasterObject.canMove = true;
        PlayerObject.canMove = true;
        victoryManager.MasterObject = MasterObject;
        victoryManager.PlayerObject = PlayerObject;
        LoadingScreen.SetActive(false);
    }

    //Spawn the player using the online instantiation
    public GameObject OnlineSpawn(bool Master, string PrefabName)
    {
        //coded only for two
        if (Master)
        {
            return PhotonNetwork.Instantiate(PrefabName, SpawnPoints[1].transform.position, SpawnPoints[1].transform.rotation);
        }
        else
        {
            return PhotonNetwork.Instantiate(PrefabName, SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
        }
    }

}

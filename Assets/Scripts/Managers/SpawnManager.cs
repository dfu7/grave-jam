using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviourPunCallbacks
{

    public static SpawnManager Instance;

    private static GameObject[] SpawnPoints;// = new Transform[2];

    private PlayerController[] playerObjects;
    private PlayerController MasterObject;
    private PlayerController PlayerObject;

    [SerializeField] private NewAudioManager newAudioManager;

    [SerializeField] private VictoryManager victoryManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject LoadingScreen;

    [HideInInspector] public bool ObjectsSet;
    
    [SerializeField] private GameObject Countdown;
    [SerializeField] private TMPro.TMP_Text countdownNum;

    private PhotonView view;

    //set the static variables
    private void Awake()
    {
        Instance = new SpawnManager();      //may need to edit the way this is done if we need to set the variables for this version

        SpawnPoints = new GameObject[2];
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        playerObjects = FindObjectsOfType<PlayerController>();

        LoadingScreen.SetActive(true);

        ObjectsSet = false;

        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!ObjectsSet && playerObjects != null && playerObjects.Length == 2)
        {
            if (playerObjects[0].gameObject.name == "playa1")
            {
                //found "playa1" player as the master
                if (PhotonNetwork.IsMasterClient)
                {
                    MasterObject = playerObjects[0];
                    PlayerObject = playerObjects[1];
                    Instance.MasterObject = playerObjects[0];
                    Instance.PlayerObject = playerObjects[1];
                }
                else
                {
                    MasterObject = playerObjects[1];
                    PlayerObject = playerObjects[0];
                    Instance.MasterObject = playerObjects[1];
                    Instance.PlayerObject = playerObjects[0];
                }
            }
            else
            {
                //found "not my" player as the master
                if (PhotonNetwork.IsMasterClient)
                {
                    MasterObject = playerObjects[1];
                    PlayerObject = playerObjects[0];
                    Instance.MasterObject = playerObjects[1];
                    Instance.PlayerObject = playerObjects[0];
                }
                else
                {
                    MasterObject = playerObjects[0];
                    PlayerObject = playerObjects[1];
                    Instance.MasterObject = playerObjects[0];
                    Instance.PlayerObject = playerObjects[1];
                }
            }
            BothIn();
        }
        else if(!ObjectsSet)
        {
            playerObjects = FindObjectsOfType<PlayerController>();
            Debug.Log("lookingstill");
        }
    }

    public void BothIn()
    {
        ObjectsSet = true;
        Instance.ObjectsSet = true;
        StartCoroutine(StartSequence());
        victoryManager.MasterObject = MasterObject;
        victoryManager.PlayerObject = PlayerObject;
        scoreManager.MasterObject = MasterObject;
        scoreManager.PlayerObject = PlayerObject;
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

    public IEnumerator StartSequence()
    {
        LoadingScreen.SetActive(false);
        newAudioManager.PlayStartCountdown();
        Debug.Log("loading screen deactivated");
        //yield return new WaitForSeconds(1);
        Countdown.SetActive(true);
        //Debug.Log("3");
        yield return new WaitForSeconds(1);
        countdownNum.text = "Ready";
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        countdownNum.text = "Set";
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        countdownNum.text = "Go!";
        Debug.Log("dig");
        yield return new WaitForSeconds(0.5f);
        countdownNum.enabled = false;
        // play awesome go sound
        MasterObject.canMove = true;
        PlayerObject.canMove = true;
        Debug.Log("sequence finished, players can move");
    }
}

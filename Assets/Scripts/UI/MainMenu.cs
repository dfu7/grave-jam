using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainMenu : MonoBehaviourPunCallbacks
{
    //this is the script that handles the buttons in the main menu

    [SerializeField] private string ConnectToServer;

    private bool OneFrame = false;

    private void Update()
    {
        //calls all of the photon leave room an disconnections
        if (!OneFrame && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            OneFrame = true;
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        //Debug.LogError("Disconnect Called");
    }

    //Enter the Lobby
    public void OnClickOnlineMultiplayer()
    {
        PhotonNetwork.LoadLevel(ConnectToServer);
    }

    //Quit the Game
    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text RoomName;
    private LobbyManager Manager;

    //public string Password;

    private void Start()
    {
        Manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _RoomName)
    {
        RoomName.text = _RoomName;
    }

    public void OnClickItem()
    {
        //This is only relevant if we use a password
        /*if (Password == null || Password.Equals(""))
        {
            Manager.JoinRoom(RoomName.text);
        }
        else
        {
            Manager.ActivatePasswordPanel(RoomName.text, Password);
        }*/

        //and will be replaced with just this
        Manager.JoinRoom(RoomName.text);
    }
}

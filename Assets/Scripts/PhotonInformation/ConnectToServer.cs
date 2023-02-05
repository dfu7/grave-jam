using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private NewAudioManager newAudioManager;
    [SerializeField] private TMPro.TMP_InputField UsernameInput;
    [SerializeField] private TMPro.TMP_Text ButtonText;
    [SerializeField] private string MainMenuName;
    [SerializeField] private string LobbyName;

    private bool Connecting = false;

    string[] regions = { "asia", "au", "cae", "eu", "in", "jp", "ru", "rue", "za", "sa", "kr", "tr", "us", "usw" };

    int region = 12;

    public void OnClickConnect()
    {
        if (UsernameInput.text.Length >= 1 && UsernameInput.text.Length <= 12)
        {
            PhotonNetwork.NickName = UsernameInput.text;
            ButtonText.text = "Connecting...";
            Connecting = true;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regions[region];
            Debug.Log(regions[region]);
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.CloudRegion);
        Debug.Log(PhotonNetwork.GetPing());

        PhotonNetwork.LoadLevel(LobbyName);
    }

    public void OnClickBack()
    {
        if (!Connecting)
        {
            newAudioManager.PlayButtonCancel();
            StartCoroutine(BackClicked());
        }
    }

    public IEnumerator BackClicked()
    {
        yield return new WaitForSeconds(newAudioManager.TimeToWait);
        PhotonNetwork.LoadLevel(MainMenuName);
    }

    public void ChangeRegion(int i)
    {
        region = i;
    }
}

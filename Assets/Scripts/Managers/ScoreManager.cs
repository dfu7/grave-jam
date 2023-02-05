using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public PlayerController MasterObject;
    public PlayerController PlayerObject;

    [SerializeField] private TMPro.TMP_Text MasterScore;
    [SerializeField] private TMPro.TMP_Text PlayerScore;

    // Update is called once per frame
    void Update()
    {
        if (MasterObject != null)
        {
            MasterScore.text = "Player 1 Score: " + MasterObject.Score;
        }

        if(PlayerObject != null)
        {
            PlayerScore.text = "Player 2 Score: " + PlayerObject.Score;
        }
    }
}

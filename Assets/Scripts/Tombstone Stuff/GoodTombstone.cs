using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GoodTombstone : Tombstone
{
    public override void Effect(PlayerController PlayerObject)
    {
        Debug.Log("i love everything1! happy  :^)");

        GameObject c = PhotonNetwork.Instantiate("coin", transform.position, Quaternion.identity);
        Animator canimator = c.GetComponentInChildren<Animator>();
        canimator.Play("happy_coin");

        PlayerObject.GainCoin();
        RemoveTombstone(false);
    }
}

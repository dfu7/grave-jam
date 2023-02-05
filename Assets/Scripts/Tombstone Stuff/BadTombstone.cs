using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BadTombstone : Tombstone
{
    public override void Effect(PlayerController PlayerObject)
    {
        Debug.Log("bad really super mean >:((( ");

        GameObject g = PhotonNetwork.Instantiate("Ghost", transform.position, Quaternion.identity);
        Animator ganimator = g.GetComponentInChildren<Animator>();
        ganimator.Play("attack_ghost");

        PlayerObject.GetStunned(g);
        RemoveTombstone(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BadTombstone : Tombstone
{
    [SerializeField] private float SecondsOfStun;

    public override void Effect(PlayerController PlayerObject)
    {
        Debug.Log("bad really super mean >:((( ");
        StartCoroutine(StunPlayer(PlayerObject));
        RemoveTombstone(false);
    }

    private IEnumerator StunPlayer(PlayerController PlayerObject)
    {
        PlayerObject.canMove = false;
        yield return new WaitForSeconds(SecondsOfStun);
        PlayerObject.canMove = true;
    }
}

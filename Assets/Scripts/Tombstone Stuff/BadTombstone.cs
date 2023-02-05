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
        PlayerObject.GetStunned();
        RemoveTombstone(false);
    }
}

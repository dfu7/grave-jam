using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GoodTombstone : Tombstone
{
    public override void Effect(PlayerController PlayerObject)
    {
        PlayerObject.GainCoin();
        RemoveTombstone(false);
        Debug.Log("i love everything1! happy  :^)");
    }
}

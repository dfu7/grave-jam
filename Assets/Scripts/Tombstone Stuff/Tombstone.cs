using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class Tombstone : MonoBehaviour
{
    protected PhotonView view;

    protected void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public abstract void Effect(PlayerController Object);
}

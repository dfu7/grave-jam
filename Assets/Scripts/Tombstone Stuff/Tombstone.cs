using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class Tombstone : MonoBehaviour
{
    public bool Selected;

    protected PhotonView view;

    protected void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public abstract void Effect(PlayerController Object);

    protected void RemoveTombstone(bool ShouldDestroy)
    {
        if(ShouldDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void Select()
    {
        view.RPC("RPC_Select", RpcTarget.All);
    }

    public void Deselect()
    {
        view.RPC("RPC_Deselect", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_Select()
    {
        Selected = true;
    }

    [PunRPC]
    public void RPC_Deselect()
    {
        Selected = false;
    }
}

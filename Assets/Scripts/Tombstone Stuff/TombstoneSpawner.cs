using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;

public class TombstoneSpawner : MonoBehaviourPunCallbacks
{
    public int numOfGoodTombstones = 19;
    public int numOfBadTombstones = 6;
    [SerializeField] private string goodTombstoneName;
    [SerializeField] private string badTombstoneName;
    [SerializeField] private Tombstone goodTombstone;
    [SerializeField] private Tombstone badTombstone;
    [SerializeField] private GameObject graveyard;

    private float gWidthR;
    private float gLengthR;
    private float baseSpawnY;

    public bool UsingPhoton;

    private void Awake()
    {   // might subtract 1 from these to give space for fence
        gWidthR = graveyard.GetComponent<MeshRenderer>().bounds.size.x / 2 - 2;
        gLengthR = graveyard.GetComponent<MeshRenderer>().bounds.size.z / 2 - 2;
        baseSpawnY = graveyard.transform.position.y + graveyard.GetComponent<MeshRenderer>().bounds.size.y + 0.3f;

        if (!UsingPhoton)
        {
            for (int i = 0; i < numOfGoodTombstones; i++)
            {
                Instantiate(goodTombstone, randomPos(), Quaternion.identity);
            }

            for (int i = 0; i < numOfBadTombstones; i++)
            {
                Instantiate(badTombstone, randomPos(), Quaternion.identity);
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                
                for (int i = 0; i < numOfGoodTombstones; i++)
                {
                    PhotonNetwork.Instantiate(goodTombstoneName, randomPos(), Quaternion.identity);
                }

                for (int i = 0; i < numOfBadTombstones; i++)
                {
                    PhotonNetwork.Instantiate(badTombstoneName, randomPos(), Quaternion.identity);
                }
            }  
        } 
    }

    private Vector3 randomPos()
    {
        int x = (int)Random.Range(-gWidthR, gWidthR);
        int z = (int)Random.Range(-gLengthR + 1, gLengthR);
        return new Vector3(x, baseSpawnY, z);
    }

}

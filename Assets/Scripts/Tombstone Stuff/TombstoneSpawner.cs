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
                SpawnTombstone(goodTombstone);
            }

            for (int i = 0; i < numOfBadTombstones; i++)
            {
                SpawnTombstone(badTombstone);
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                
                for (int i = 0; i < numOfGoodTombstones; i++)
                {
                    PhotonSpawnTombstone(goodTombstoneName);
                }

                for (int i = 0; i < numOfBadTombstones; i++)
                {
                    PhotonSpawnTombstone(badTombstoneName);
                }
            }  
        } 
    }

    public void SpawnTombstone(Tombstone tombstone)
    {
        Tombstone ts = Instantiate(tombstone, randomPos(), Quaternion.identity);

        if (ts.GetComponent<TombstoneCollision>().collided)
        {
            Destroy(ts.gameObject);
            SpawnTombstone(tombstone);
        }
    }

    public void PhotonSpawnTombstone(string tombstoneName)
    {
        GameObject ts = PhotonNetwork.Instantiate(tombstoneName, randomPos(), Quaternion.identity);


        if (ts.GetComponent<TombstoneCollision>().collided)
        {
            PhotonNetwork.Destroy(ts.gameObject);
            PhotonSpawnTombstone(tombstoneName);
        }
    }

    private Vector3 randomPos()
    {
        int x = (int)Random.Range(-gWidthR, gWidthR);
        int z = (int)Random.Range(-gLengthR + 1, gLengthR);
        return new Vector3(x, baseSpawnY, z);
    }
}

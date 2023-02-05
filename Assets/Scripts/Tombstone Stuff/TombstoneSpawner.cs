using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class TombstoneSpawner : MonoBehaviourPunCallbacks
{
    public int numOfGoodTombstones = 19;
    public int numOfBadTombstones = 6;
    [SerializeField] private string goodTombstoneName;
    [SerializeField] private string badTombstoneName;
    [SerializeField] private GameObject goodTombstone;
    [SerializeField] private GameObject badTombstone;
    [SerializeField] private GameObject graveyard;
    private GameObject[] trees;
    private List<GameObject> tombstones = new List<GameObject>();

    private float gWidthR;
    private float gLengthR;
    private float baseSpawnY;

    public bool UsingPhoton;

    private void Awake()
    {   // might subtract 1 from these to give space for fence
        gWidthR = graveyard.GetComponent<MeshRenderer>().bounds.size.x / 2 - 2;
        gLengthR = graveyard.GetComponent<MeshRenderer>().bounds.size.z / 2 - 2;
        baseSpawnY = graveyard.transform.position.y + graveyard.GetComponent<MeshRenderer>().bounds.size.y + 0.3f;

        trees = GameObject.FindGameObjectsWithTag("Tree");

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

    public void SpawnTombstone(GameObject tombstone)
    {
        GameObject ts = Instantiate(tombstone, randomPos(), Quaternion.identity);

        if (TouchesAnything(ts.GetComponent<Collider>()))
        {
            Destroy(ts.gameObject);
            SpawnTombstone(tombstone);
        }
        else
        {
            tombstones.Add(ts);
        }
    }

    public void PhotonSpawnTombstone(string tombstoneName)
    {
        Debug.Log("spawn tombstone called");
        GameObject ts = PhotonNetwork.Instantiate(tombstoneName, randomPos(), Quaternion.identity);

        if (TouchesAnything(ts.GetComponent<Collider>()))
        {
            Debug.Log("was touching");
            PhotonNetwork.Destroy(ts.gameObject);
            PhotonSpawnTombstone(tombstoneName);
        }
        else
        {
            Debug.Log("not touching anything; tombstone added");
            tombstones.Add(ts);
        }
    }

    private Vector3 randomPos()
    {
        int x = (int)Random.Range(-gWidthR, gWidthR);
        int z = (int)Random.Range(-gLengthR + 1, gLengthR);
        return new Vector3(x, baseSpawnY, z);
    }

    private bool TouchesAnything(Collider tombstoneCollider)
    {
        Debug.Log("checking if touches anything");
        foreach (GameObject tree in trees)
        {
            // checks if the closest point in tombstone collider is within the bounds of the tree mesh renderer
            Vector3 closestPointToTree = tombstoneCollider.ClosestPoint(tree.transform.position);

            if (tree.GetComponentInChildren<MeshRenderer>().bounds.Contains(closestPointToTree))
            {
                return true;
            }
        }
        Debug.Log("not touching tree");

        foreach (GameObject ts in tombstones)
        {
            Debug.Log("-assign var");
            Vector3 closestPointToTs = tombstoneCollider.ClosestPoint(ts.transform.position);
            Debug.Log("-get collider");
            if (ts.GetComponent<Collider>().bounds.Contains(closestPointToTs))
            {
                return true;
            }
            Debug.Log("-end");
        }
        Debug.Log("not touching tombstones");

        return false;
    }
}

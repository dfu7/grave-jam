using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TombstoneSpawner : MonoBehaviour
{
    public int numOfGoodTombstones = 19;
    public int numOfBadTombstones = 6;
    [SerializeField] private Tombstone goodTombstone;
    [SerializeField] private Tombstone badTombstone;
    [SerializeField] private GameObject graveyard;

    private float gWidthR;
    private float gLengthR;
    private float baseSpawnY;

    private void Awake()
    {   // might subtract 1 from these to give space for fence
        gWidthR = graveyard.GetComponent<MeshRenderer>().bounds.size.x / 2 - 2;
        gLengthR = graveyard.GetComponent<MeshRenderer>().bounds.size.z / 2 - 2;
        baseSpawnY = graveyard.transform.position.y + graveyard.GetComponent<MeshRenderer>().bounds.size.y + 0.3f;

        for (int i = 0; i < numOfGoodTombstones; i++)
        {
            Instantiate(goodTombstone, randomPos(), Quaternion.identity);
        }

        for (int i = 0; i < numOfBadTombstones; i++)
        {
            Instantiate(badTombstone, randomPos(), Quaternion.identity);
        }
    }

    private Vector3 randomPos()
    {
        int x = (int)Random.Range(-gWidthR, gWidthR);
        int z = (int)Random.Range(-gLengthR + 1, gLengthR);
        return new Vector3(x, baseSpawnY, z);
    }

}

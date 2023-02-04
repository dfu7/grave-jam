using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private bool CollisionsIgnored = false;

    public void Update()
    {
        if(!CollisionsIgnored && SpawnManager.Instance.ObjectsSet)
        {
            PlayerController[] Players = FindObjectsOfType<PlayerController>();

            if (Players[0] == gameObject.GetComponent<PlayerController>())
            {
                Physics.IgnoreCollision(Players[1].GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                //Debug.LogError("IgnoringPlayer[1],then0");
            }
            else
            {
                Physics.IgnoreCollision(Players[0].GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                //Debug.LogError("IgnoringPlayer[0],then1");
            }
            CollisionsIgnored = true;
        }
        else
        {
            Debug.Log(CollisionsIgnored + " and " + SpawnManager.Instance.ObjectsSet);
        }
    }
}

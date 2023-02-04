using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Vector3 movement;
    [SerializeField] private float speed = 8;
    [SerializeField] private float rotationSpeed = 720;
    public bool canMove = true;
    private bool casting = false;

    [SerializeField] private float inRangeDistance = 1;
    private float tombstoneHeldTime = 0;
    bool pulling = false;

    public int Score;

    private Rigidbody rb;
    PhotonView view;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector3>();
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        /*
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        // everything below only happens when raycast hits and is close enough
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Tombstone")
            {
                //Debug.Log("boom");
            }
        }*/


        //pressed
        if (context.started)
        {
            pulling = true;
            //canMove = false;
            // start animation
        }

        //released
        if (context.performed)
        {
            pulling = false;
            // pause animation
        }
    }

    private void Update()
    {

        if (pulling)
        {
            tombstoneHeldTime += Time.deltaTime;
        }
        
        if ( tombstoneHeldTime >= 3 /*animation length*/ )
        {
            canMove = true;
        }
    }

    private void FixedUpdate()
    {
        if(view.IsMine)
        {
            if (movement != Vector3.zero && canMove)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), rotationSpeed * Time.deltaTime);

                transform.Translate(movement * speed * Time.deltaTime, Space.World);
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }

    }

    public void GainCoin()
    {
        view.RPC("RPC_GainCoin", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_GainCoin()
    {
        Score++;
    }
}

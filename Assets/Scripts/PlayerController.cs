using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Vector3 movement;
    [SerializeField] private float speed = 8;
    [SerializeField] private float rotationSpeed = 720;
    [SerializeField] private float SecondsOfStun;
    public bool canMove = true;

    private float inRangeDistance;
    private float tombstoneHeldTime = 0;
    bool pulling = false;
    bool checking = false;
    bool inRange = false;

    Vector3 origin;

    public int Score;

    private Rigidbody rb;

    PhotonView view;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();

        inRangeDistance = GetComponent<CapsuleCollider>().bounds.size.z / 2 + 0.2f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector3>();
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        // e pressed
        if (context.started)
        {
            if (!pulling)
            {
                checking = true; // raycast check (automatically sets checking to false) raycasts are stupid
            }
        }

        //released
        if (context.performed || !pulling)
        {
            // only !pulling if pulling finished
            if (!pulling)
            {
                // start effect
            }
            // paused pulling
            else
            {
                // pause pulling animation
            }
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
            pulling = false;
        }
    }

    private void FixedUpdate()
    {

        if (view.IsMine)
        {
            // checks if tombstone is in range and then sets inRange respectively
            if (checking)
            {
                Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, inRangeDistance))
                {
                    // check if tombstone is selected through hitInfo
                    Debug.Log("hit");
                    Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);

                    pulling = true;
                    //canMove = false;

                    Tombstone tombstone = hitInfo.collider.gameObject.GetComponent<Tombstone>();
                    if(tombstone != null && !tombstone.Selected)
                    {
                        hitInfo.collider.gameObject.GetComponent<Tombstone>().Select();
                        tombstone.Effect(this);
                    }
                    // set tombstone bool to being used';;; prob has to do with hitInfo down below vv
                    // start pulling animation
                }
                else
                {
                    Debug.Log("not hit" + transform.position);
                    Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.green);
                    inRange = false;
                }
                checking = false;
            }

            //general movement
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

    public void GetStunned()
    {
        StartCoroutine(Stun());
    }

    public IEnumerator Stun()
    {
        canMove = false;
        yield return new WaitForSeconds(SecondsOfStun);
        canMove = true;
    }

    [PunRPC]
    public void RPC_GainCoin()
    {
        Score++;
    }
}

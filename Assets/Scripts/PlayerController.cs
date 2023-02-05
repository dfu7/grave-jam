using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.UI.Image;
using System.Security.Cryptography.X509Certificates;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Vector3 movement;
    [SerializeField] private float speed = 8;
    [SerializeField] private float rotationSpeed = 720;
    [SerializeField] private float SecondsOfStun;
    public bool canMove = true;

    private float inRangeDistance;
    private float tombstoneHeldTime = 0;
    private int pulls = 0;
    private bool pulling = false;
    private bool checking = false;
    private bool response = false;
    private bool waited = false;
    private bool EffectReady = true;

    public int Score;

    private Rigidbody rb;

    private Animator animator;
    private Animator TSanimator;
    private string tombstoneChoiceState;
    Tombstone tombstone;

    PhotonView view;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();

        inRangeDistance = GetComponent<CapsuleCollider>().bounds.size.z / 2 + 0.2f;

        animator = GetComponentInChildren<Animator>();
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
            /*else if (pulls == 3)
            {
                animator.speed = 1;
                tombstone.Effect(this);
                pulls = 0;
            }
            else
            {
                ContinueAnimation();
                // check that its not playing
            }*/
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public IEnumerator ContinueAnimation()
    {
        Debug.Log("animation continued");

        if (pulls == 0)
            animator.SetBool("pulling", true);
            yield return new WaitForSeconds((1f / 331) * 31);
        if (pulls == 1)
            animator.speed = 1;
            yield return new WaitForSeconds((1f / 331) * 66);
        if (pulls == 2)
            animator.speed = 1;
            yield return new WaitForSeconds((1f / 331) * 197);

        Debug.Log("animation stopped");
        animator.speed = 0;
        pulls++;
    }

    private void Update()
    {
        /*if (pulling)
        {
            tombstoneHeldTime += Time.deltaTime;
        }
        
        if ( tombstoneHeldTime >= 3 /*animation length )
        {
            pulling = false;
        }*/
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

                    tombstone = hitInfo.collider.gameObject.GetComponent<Tombstone>();
                    if(tombstone != null && !tombstone.Selected)
                    {
                        TSanimator = tombstone.GetComponentInChildren<Animator>();
                        Debug.Log("TOMBSTONE GOOD");
                        tombstone.Select(); // set tombstone bool to being used
                        pulling = true;
                        canMove = false;
                        Debug.Log("canMove:" + canMove);
                        animator.SetBool("pulling", pulling);
                        TSanimator.SetBool("pulling", pulling);
                        animator.SetBool("good", (tombstone is GoodTombstone) ? true : false);
                        StartCoroutine(WaitASec());
                    }
                }
                checking = false;
            }

            //general movement
            if (movement != Vector3.zero && canMove)
            {
                animator.SetBool("moving", true); // movement starts

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), rotationSpeed * Time.deltaTime);
                transform.Translate(movement * speed * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetBool("moving", false); //movement stops
                rb.angularVelocity = Vector3.zero;
            }

            //Reset the player canMove
            if(pulling && waited)
            {
                if(EffectReady)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("happy_playa") && EffectReady)
                    {
                        tombstone.Effect(this);
                        EffectReady = false;
                    }
                    else if (animator.GetCurrentAnimatorStateInfo(0).IsName("stun_playa") && EffectReady)
                    {
                        tombstone.Effect(this);
                        EffectReady = false;
                    }
                }

                if(animator.GetCurrentAnimatorStateInfo(0).IsName("idel_playa"))
                {
                    Debug.LogError("I THINK ITS IDEL");
                    canMove = true;
                    pulling = false;
                    animator.SetBool("pulling", false); 
                    animator.SetBool("stunover", false);
                    EffectReady = true;
                    waited = false;
                }
            }
            else
            {
                Debug.LogWarning("pulling is " + pulling + " waited is " + waited);
            }
        }
    }

    public IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(1f);
        waited = true;
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
        animator.SetBool("stunover", true);
    }

    [PunRPC]
    public void RPC_GainCoin()
    {
        Score++;
    }
}

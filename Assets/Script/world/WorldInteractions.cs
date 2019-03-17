using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class WorldInteractions : MonoBehaviour
{
    NavMeshAgent playerAgent;


    // for animation
    private Animator animator;
    private int speedId;
    private int rotateId;


    public bool pathReached;

    // enums for animation
    public enum MoveFSM
    {
        findPosition,
        move
    }

    public MoveFSM moveFSM;



    public static WorldInteractions Instance { get; set; }


    //
    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = this.gameObject.transform.Find("Character").GetComponent<Animator>();
        animator.SetBool("isPathReach", true);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();

        }

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D)) {
            playerAgent.enabled = false;
           
        }
        else
        {
            playerAgent.enabled = true;
        }
        MoveStates();
    }
    // for animation check the states
    public void MoveStates()
    {
        switch (moveFSM)
        {
            // this is no use for now reserve incase for game willing to develop, so we can have turn etc etc for now we will use just move for simplicity
            case MoveFSM.findPosition:

                break;
            case MoveFSM.move:
                Move();
                break;
            
        }
    }


    public void Move()
    {
        if (!playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {



                pathReached = true;
                animator.SetBool("isPathReach", pathReached);
     
            }
        }

    }

   

    void GetInteraction()
    {
        //for getting the point click store to this value
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // typical raycast stuff
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            // if clicked on a npc
            if (interactedObject.tag == "Interactable Object")
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);

            }
            // here we will call player to stop when click a obstacle
            else if (interactedObject.tag == "Obstacle")
            {
               
                pathReached = true;
                animator.SetBool("isPathReach", pathReached);
             
                playerAgent.destination = playerAgent.transform.position;
            }
            else
            {
                // register the point then move the player
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;

               
                pathReached = false;
                animator.SetTrigger("isRunning");
                animator.SetBool("isPathReach", pathReached);
                moveFSM = MoveFSM.move;

            }
        }

    }
}

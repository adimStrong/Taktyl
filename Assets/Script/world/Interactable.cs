using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent playerAgent;
    private bool hasInteracted;

    // this will will take the player agent then gonna move to player to the interact object and we will do the interaction/conversaion
    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
       
        hasInteracted = false;
        // player agent on this instance not on the world interaction code
        this.playerAgent = playerAgent;
        // setting interaction to false once we interact with object with interactable tag
       
        playerAgent.stoppingDistance = 4f;
        playerAgent.destination = this.transform.position;

      
    }

    private void Update()
    {
        // check if player has a current path ongoing
        if(!hasInteracted  && playerAgent != null && !playerAgent.pathPending)
        {
            // checking the distance between the point clicked
            if (playerAgent.remainingDistance < playerAgent.stoppingDistance)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {

        Debug.Log("Has interacted with base class");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteractions : MonoBehaviour
{
    NavMeshAgent playerAgent;


    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();

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
            if(interactedObject.tag == "Interactable Object")
            {
                Debug.Log("Interactable object");
            }
            else
            {
                // register the point
                playerAgent.destination = interactionInfo.point;

            }
        }

    }
}

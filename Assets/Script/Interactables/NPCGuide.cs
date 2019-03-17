using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGuide : ActionItem
{
    public string[] dialogue;
    public string name;


    public override void Interact()
    {
        Debug.Log("Interacting with NPC Guide");
        DialogueSystems.Instance.AddNewDialogue(dialogue, name);
    }
}

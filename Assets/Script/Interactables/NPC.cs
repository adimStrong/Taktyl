using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPC : Interactable
{
    public string[] dialogue;
    public string name;


 public override void Interact()
    {
        // so we create the instance from this line of code then access the add new dialogue 
        Debug.Log("Interacting with NPC Class");
        DialogueSystems.Instance.AddNewDialogue(dialogue, name);
    }
}

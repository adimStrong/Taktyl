using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystems : MonoBehaviour
{
    public static DialogueSystems Instance{ get; set; }

    public GameObject dialoguePanel;

    public List<string> dialogueLines = new List<string>();

    public string npcName;

    Text dialogueText, nameText;
    Button continueBtn;
    int dialogueIndex;

    private void Awake()
    {
        continueBtn = dialoguePanel.transform.Find("Continue").GetComponent<Button>();

        dialogueText = dialoguePanel.transform.Find("DialogueText").GetComponent<Text>();

        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();

        // hide dialogue panel at start
        dialoguePanel.SetActive(false);

        continueBtn.onClick.AddListener(delegate { ContinueDialogue(); });
        // check if the instance is already create destroy on awake if has any then create an instance
        if(Instance != null  && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public void AddNewDialogue(string [] lines, string npcName)
    {
        // set diagloueIndex 0 when adding new dialogue
        dialogueIndex = 0;
        //  add list of string to dialogue lines, so we can create the dialogue for the npc
        dialogueLines = new List<string>();
        foreach (string line in lines) 
        {
            dialogueLines.Add(line);
        }
        // setting npc name global var to this method npc name
        this.npcName = npcName;
        Debug.Log(dialogueLines.Count);
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        // add code to stop player from moving to be continue
    }

    public void ContinueDialogue()
    {
        if(dialogueIndex < dialogueLines.Count -1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];

        }
        else
        {
            dialoguePanel.SetActive(false);
            // add code to continue player from moving to be continue
        }
    }
}

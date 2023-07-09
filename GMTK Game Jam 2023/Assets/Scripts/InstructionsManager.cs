using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{

    [SerializeField] DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        string[] instructionsDialogue = {
            "You have been selected to be on a game show.",
            "Congratulations!",
            "What's that?",
            "You thought that you would be a contestant?",
            "Actually, you are going to be the show's host.",
            "Your job as the show's host is to pick questions to ask the contestants.",
            "Each question choice will have a difficulty rating and an \"EV\" rating.\nasdfasdfsa",

        };
        dialogueManager.HandleDialogue(instructionsDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

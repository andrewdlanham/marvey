using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InstructionsManager : MonoBehaviour
{

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        string[] instructionsDialogue = {
            "Hello, Steve Marvey.",
            "You have been selected to be on a game show.",
            "Congratulations!",
            "What's that?",
            "You thought that you would be a contestant?",
            "Actually, you are going to be the show's host.",
            "Your job as the show's host is to pick questions to ask the contestants.",
            "Each question choice will have a difficulty rating and an \"EV\" rating.",
            "This \"EV\" stands for \"Entertainment Value\".",
            "Questions with a higher \"EV\" are more entertaining and will gain the show higher ratings.",
            "Questions with a higher difficulty will be more difficult for the contestant to correctly answer.",
            "The difficulty of questions will generally increase throughout the episode.",
            "If you fail to meet the required ratings for the episode or if the contestant gets 3 questions wrong, it's game over for you.",
            "We will not hesitate to replace you, Marvey.",
            "Good luck."
        };
        dialogueManager.HandleDialogue(instructionsDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameManager.GameState.INSTRUCTIONSDONE) {
            gameManager.currentGameState = GameManager.GameState.INSTRUCTIONS;
            StartCoroutine(loadLevel1());
        }
    }


    IEnumerator loadLevel1() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level1");
    }

}

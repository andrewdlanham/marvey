using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutroManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        string[] outroDialogue = {
            "Wow!",
            "You were electric out there Steve!",
            "You are the best game show host we have ever seen!",
            "I hope you enjoyed your stay, because we are cooking up a new show for you.",
            "We will contact you about it later, but we were thinking about making a show where you are a judge.",
            "We'll call it \"Judge Marvey\" or something idk.",
            "...",
            "What are you still doing here?",
            "Go back to the glorious main menu."
        };
        dialogueManager.HandleDialogue(outroDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentGameState == GameManager.GameState.INSTRUCTIONSDONE) {
            gameManager.currentGameState = GameManager.GameState.INSTRUCTIONS;
            StartCoroutine(loadMainMenu());
        }
    }


    IEnumerator loadMainMenu() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}

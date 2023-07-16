using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    
    private Queue<string> sentenceQueue;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueTextBox;
    [SerializeField] private bool inDialogue;

    private bool isTyping;

    void Awake() {
        isTyping = false;
        inDialogue = false;
        sentenceQueue = new Queue<string>();
    }

    void Update() {
        if (isTyping) {
            return;
        } else if (inDialogue) {
            if (Input.GetMouseButtonDown(0)) {
                StopAllCoroutines();
                DisplayNextSentence();
            }
        }
    }

    public void HandleDialogue(string[] sentences) {
        inDialogue = true;
        sentenceQueue.Clear();
        enqueueSentences(sentences);
        DisplayNextSentence();

    }

    private void DisplayNextSentence() {
        if (sentenceQueue.Count == 0) {
            EndDialogue();
            return;
        }
        //StopAllCoroutines();
        StartCoroutine(TypeSentence(sentenceQueue.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence) {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
        isTyping = false;
    }

    private void EndDialogue() {
        
        inDialogue = false;
        switch (gameManager.currentGameState) {
            case GameManager.GameState.DIALOGUE1:
                Debug.Log("Dialogue 1 over");
                gameManager.currentGameState = GameManager.GameState.CHOICES;
                break;
            case GameManager.GameState.DIALOGUE2:
                Debug.Log("Dialogue 2 over");
                gameManager.currentGameState = GameManager.GameState.DIALOGUE3;
                StartCoroutine(gameManager.handleResponse());
                break;
            case GameManager.GameState.DIALOGUE3:
                Debug.Log("Dialogue 3 over");
                gameManager.startNewRound();
                break;
            case GameManager.GameState.INSTRUCTIONS:
                Debug.Log("Instructions over");
                gameManager.currentGameState = GameManager.GameState.INSTRUCTIONSDONE;
                break;
        }
        
    }


    private void enqueueSentences(string[] sentences) {
        foreach (string sentence in sentences) {
            sentenceQueue.Enqueue(sentence);
        }
    }
    
    public void showDialogueBox() {
        dialogueTextBox.SetActive(true);
    }

    public void hideDialogueBox() {
        dialogueTextBox.SetActive(false);
    }
}
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

    void Start() {
        inDialogue = false;
        sentenceQueue = new Queue<string>();
    }

    void Update() {
        if (inDialogue) {
            if (Input.GetKeyDown("space")) {
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
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue() {
        Debug.Log("EndDialogue()");
        inDialogue = false;
        if (gameManager.currentGameState == GameManager.GameState.DIALOGUE1) {
            gameManager.currentGameState = GameManager.GameState.CHOICES;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] public enum GameState {PICKINGQUESTION, QUESTIONPICKED, DIALOGUE1, CHOICES, DIALOGUE2};

    public GameState currentGameState;

    public QuestionManager.Question pickedQuestion;


    [SerializeField] public QuestionManager questionManager;
    [SerializeField] public DialogueManager dialogueManager;

    public QuestionManager.Question[] questionChoices;

    private int curRound;
    private int targetRound;
    private int numWrongGuesses;

    private int curShowRating;

    private int isPicking;

    private int pickedQuestionNum;

    private void showQuestionButtons() {
        q1Button.gameObject.SetActive(true);
        q2Button.gameObject.SetActive(true);
        q3Button.gameObject.SetActive(true);
        q4Button.gameObject.SetActive(true);
    }

    [SerializeField] Button q1Button;
    [SerializeField] Button q2Button;
    [SerializeField] Button q3Button;
    [SerializeField] Button q4Button;
    [SerializeField] TextMeshProUGUI q1ButtonText;
    [SerializeField] TextMeshProUGUI q2ButtonText;
    [SerializeField] TextMeshProUGUI q3ButtonText;
    [SerializeField] TextMeshProUGUI q4ButtonText;
    [SerializeField] GameObject c1Panel;
    [SerializeField] TextMeshProUGUI c1PanelText;
    [SerializeField] GameObject c2Panel;
    [SerializeField] TextMeshProUGUI c2PanelText;
    [SerializeField] GameObject c3Panel;
    [SerializeField] TextMeshProUGUI c3PanelText;
    [SerializeField] GameObject c4Panel;
    [SerializeField] TextMeshProUGUI c4PanelText;
    

    private void handleQuestionButtonUI() {

        Button[] buttons = {q1Button, q2Button, q3Button, q4Button};
        TextMeshProUGUI[] texts = {q1ButtonText, q2ButtonText, q3ButtonText, q4ButtonText};
        for (int i = 0; i < 4; i++) {
            Button curButton = buttons[i];
            TextMeshProUGUI curText = texts[i];
            QuestionManager.Question curQuestion = questionChoices[i];
            curButton.onClick.AddListener(() => questionButtonOnClick(curQuestion));
            curText.text = "";
            curText.text += "Q" + (i+1) + "\n";
            curText.text += "Diff: " + questionChoices[i].difficulty + "\n";
            curText.text += "EV: " + questionChoices[i].entertainmentValue + "\n";
        }
        currentGameState = GameState.PICKINGQUESTION;
        showQuestionButtons();
    }

    public void questionButtonOnClick(QuestionManager.Question question) {
        Debug.Log("Question button clicked!");
        pickedQuestion = question;
        currentGameState = GameState.QUESTIONPICKED;
    }

    private void hideQuestionButtons() {
        q1Button.gameObject.SetActive(false);
        q2Button.gameObject.SetActive(false);
        q3Button.gameObject.SetActive(false);
        q4Button.gameObject.SetActive(false);
    }

    #region Choice Panels
    private void updateChoicePanels() {
        c1PanelText.text = pickedQuestion.choices[0];
        c2PanelText.text = pickedQuestion.choices[1];
        c3PanelText.text = pickedQuestion.choices[2];
        c4PanelText.text = pickedQuestion.choices[3];
    }
    private void showChoicePanels() {
        c1Panel.gameObject.SetActive(true);
        c2Panel.gameObject.SetActive(true);
        c3Panel.gameObject.SetActive(true);
        c4Panel.gameObject.SetActive(true);
    }

    private void hideChoicePanels() {
        c1Panel.gameObject.SetActive(false);
        c2Panel.gameObject.SetActive(false);
        c3Panel.gameObject.SetActive(false);
        c4Panel.gameObject.SetActive(false);
    }
    #endregion

    IEnumerator handleContestantChoices() {
        yield return new WaitForSeconds(2f);
        Debug.Log("handleContestantChoices()");
        updateChoicePanels();
        showChoicePanels();
        // Handle dialogue
        string response = "";
        if (gotQuestionCorrect(pickedQuestion)) {
            response = pickedQuestion.correctResponses[0];
        } else {
            response = pickedQuestion.incorrectResponses[0];
        }

        string[] dialogueSentences = {
                "PAUL: Let's see here...",
                response
            };
        
        dialogueManager.HandleDialogue(dialogueSentences);

    }

    private bool gotQuestionCorrect(QuestionManager.Question question) {
        int correctPercent = 100;
        correctPercent -= (question.difficulty * 10);
        float randomNum = Random.Range(0, 100);
        if (randomNum < correctPercent) return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.hideDialogueBox();
        hideChoicePanels();
        hideQuestionButtons();
        questionManager.populateQuestionsList();
        curRound = 0;
        targetRound = 9;
        curShowRating = 0;
        questionChoices = questionManager.getQuestionChoices(curRound);
        handleQuestionButtonUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.QUESTIONPICKED) {
            hideQuestionButtons();
            dialogueManager.showDialogueBox();
            currentGameState = GameState.DIALOGUE1;
            string[] dialogueSentences = {
                "STEVE: Here's a question for you...",
                "STEVE: " + pickedQuestion.questionText
            };
            dialogueManager.HandleDialogue(dialogueSentences);
        }

        if (currentGameState == GameState.CHOICES) {
            currentGameState = GameState.DIALOGUE2;
            StartCoroutine(handleContestantChoices());
        }
    }
    




}

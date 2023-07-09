using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Random = UnityEngine.Random;

public class QuestionManager : MonoBehaviour
{


    public List<List<Question>> questionsList;
    

    [System.Serializable]
    public class Question {
        public string questionText;
        public string[] choices;
        public string answer;
        public int entertainmentValue;
        public int difficulty;
        public List<string> correctResponses;
        public List<string> incorrectResponses;

    }
    

    [System.Serializable]
    public class QuestionData {
        public Question[] questions;
    }
    


    public void populateQuestionsList() {

        questionsList = new List<List<Question>>();
        for (int i = 0; i < 10; i++) {
            questionsList.Add(new List<Question>{});
        }
        string questionsFilePath = Path.Combine(Application.dataPath, "questions.json");
        Debug.Log("populateQuestionsList()...");
        //Debug.Log("questionsFilePath: " + questionsFilePath);
        string data = File.ReadAllText(questionsFilePath);
        Debug.Log("data: " + data);
        QuestionData questionData = JsonUtility.FromJson<QuestionData>(data);
        Debug.Log("questionData.questions: " + questionData.questions);
        foreach (Question question in questionData.questions) {
            questionsList[question.difficulty].Add(question);
        }
    }

    private void printQuestionsList() {
        Debug.Log("printQuestionsList()...");
        foreach (List<Question> list in questionsList) {
            foreach(Question question in list) {
                Debug.Log(question.questionText + ", Diff: " + question.difficulty);
            }
        }
    }

    public Question getRandomQuestion(int round) {

        int numPossibleQuestions = questionsList[round].Count;
        //Debug.Log("round: " + round);
        //Debug.Log("numPossibleQuestions: " + numPossibleQuestions);

        int randomQuestionIndex = Random.Range(0, numPossibleQuestions);
        //Debug.Log("randomQuestionIndex: " + randomQuestionIndex);

        Question randomQuestion = questionsList[round][randomQuestionIndex];
        //Debug.Log("randomQuestion: " + randomQuestion.questionText);
        return randomQuestion;
    }

    public Question[] getQuestionChoices(int round) {
        Question[] questionChoices = new Question[4];
        for (int i = 0; i < 4; i++) {
            int difficulty = Random.Range(round - 1, round + 2);
            questionChoices[i] = getRandomQuestion(difficulty);
        }
        return questionChoices;
    }
    

}

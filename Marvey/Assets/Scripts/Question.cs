using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] choices;
    public string answer;
    public int entertainmentValue;
    public int difficulty;
    public List<string> correctResponses;
    public List<string> incorrectResponses;
}

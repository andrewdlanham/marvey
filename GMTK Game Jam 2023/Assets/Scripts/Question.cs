using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question : MonoBehaviour
{
    
    
    public string questionText;
    public string[] choices;
    public string answer;
    public int entertainmentValue;
    public int difficulty;
    public List<string> correctResponses;
    public List<string> incorrectResponses;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

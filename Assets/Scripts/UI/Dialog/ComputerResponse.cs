using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerResponse : MonoBehaviour
{
    public Dictionary<string, string> _userInputToComputerResponse = new Dictionary<string, string>()
                {
                    { "Initial", "I want a sandwich"},
                    { "How are you?", "I'm not here for conversation, I want a sandwich"},
                    { "What do you want on it?", "The User pressed the SECOND button"},
                    { "User Response 3", "The User pressed the THIRD button"},
                    { "User Response 4", "The User pressed the FOURTH button"},
                    { "Failure", "You're taking forever... Hurry up"}
                };

    public Text computerResponseText;

    public void Start()
    {
        computerResponseText = GetComponent<Text>();
    }

    public void EvaluateSandwich(float score)
    {
        if (score < .5f)
        {
            computerResponseText.text = "What the hell is this?";
        }
        else if (score < .7f)
        {
            computerResponseText.text = "Thanks";
        }
        else if (score < .9f)
        {
            computerResponseText.text = "Thanks this looks great!";
        }
        else
        {
            computerResponseText.text = "This is the prettiest sandwich I've ever seen!";
        }
    }
}

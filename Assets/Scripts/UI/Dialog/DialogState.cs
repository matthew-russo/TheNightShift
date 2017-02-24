using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls tshe dialog flow for User and Customers with two dictionaries.
/// </summary>

public class DialogState : Singleton<DialogState>
{
    public Text customerText;
    public Text userChoice1;
    public Text userChoice2;
    public UserInput userInput1;
    public UserInput userInput2;

    // Dictionary with a key of user responses and their corresponding customer responses
    //
    public Dictionary<string, string> CustomerResponseDictionary = new Dictionary<string, string>()
                {
                    { "Initial", "I want a sandwich"},
                    { "How are you?", "I'm not here for conversation, I want a sandwich"},
                    { "Don't be rude", "Your manager is going to hear about this"},
                    { "The sandwich you asked for!", "Don't be a smart ass, where's your manager?"},
                    { "Sorry... I'm new here", "Hope you get better quickly..."},
                    { "What do you want on it?", "I want a sandwich" },
                };

    // Dictionary with the computer text as key and proper user responses as values
    //
    public Dictionary<string, UserChoices> UserResponses = new Dictionary<string, UserChoices>()
                {
                    { "I want a sandwich", new UserChoices("How are you?", "What do you want on it?", "Greeting")},
                    { "What the hell is this?", new UserChoices("The sandwich you asked for!", "Sorry... I'm new here", "AngryExit")},
                    { "Thanks", new UserChoices("Have a good one", "Enjoy!", "HappyExit")},
                    { "Thanks this looks great!", new UserChoices("No problemo", "Thanks", "HappyExit")},
                    { "This is the prettiest sandwich I've ever seen!", new UserChoices("*thumbs up*", "Thanks", "HappyExit")},
                    { "I'm not here for conversation, I want a sandwich", new UserChoices("Don't be rude", "What do you want on it?", "null")},
                    { "Your manager is going to hear about this", new UserChoices("Yup", "Bite me", "AngryExit") },
                    { "Don't be a smart ass, where's your manager?", new UserChoices("Not here", "I'm the manager now", "AngryExit") },
                    { "Hope you get better quickly...", new UserChoices("Yeah", "Okay","AngryExit")},
                    { "What do you want on it?", new UserChoices("one", "two","null") },
                };

    // If in dialog state, assign the text of User Buttons based upon the current computer response
    // If in explore state, reset computer text to starting point
    //
    private void Update()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Dialog)
        {
            userChoice1.text = UserResponses[customerText.text].choice1;
            userChoice2.text = UserResponses[customerText.text].choice2;
            userInput1.currentChoices = UserResponses[customerText.text];
            userInput2.currentChoices = UserResponses[customerText.text];
        }
        else if (StateMachine.Instance.currentGameState == StateMachine.State.Explore)
        {
           customerText.text = CustomerResponseDictionary["Initial"];
        }
    }
}

// Class to hold User Response data
//
public class UserChoices
{
    public string choice1;
    public string choice2;
    public string tag;
    
    public UserChoices(string a, string b, string c)
    {
        choice1 = a;
        choice2 = b;
        tag = c;
    }
}
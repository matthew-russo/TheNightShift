using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;
using UnityEngine.UI;

public class DialogState : Singleton<DialogState>
{
    public Text computerText;
    public Text userChoice1;
    public Text userChoice2;

    public Dictionary<string, string> ComputerResponseDictionary = new Dictionary<string, string>()
                {
                    { "Initial", "I want a sandwich"},
                    { "How are you?", "I'm not here for conversation, I want a sandwich"},
                    { "Don't be rude", "Your manager is going to hear about this"},
                    { "The sandwich you asked for!", "Don't be a smart ass, where's your manager?"},
                    { "Sorry... I'm new here", "Hope you get better quickly..."},
                    { "What do you want on it?", "I want a sandwich" },
                };

    public Dictionary<string, UserChoices> UserResponses = new Dictionary<string, UserChoices>()
                {
                    { "I want a sandwich", new UserChoices("How are you?", "What do you want on it?")},
                    { "What the hell is this?", new UserChoices("The sandwich you asked for!", "Sorry... I'm new here")},
                    { "Thanks", new UserChoices("Have a good one", "Enjoy!")},
                    { "Thanks this looks great!", new UserChoices("No problemo", "Thanks")},
                    { "This is the prettiest sandwich I've ever seen!", new UserChoices("*thumbs up*", "Thanks")},
                    { "I'm not here for conversation, I want a sandwich", new UserChoices("Don't be rude", "What do you want on it?")},
                    { "Your manager is going to hear about this", new UserChoices("Yup", "Bite me") },
                    { "Don't be a smart ass, where's your manager?", new UserChoices("Not here", "I'm the manager now") },
                    { "Hope you get better quickly...", new UserChoices("Yeah", "Okay")},
                    { "What do you want on it?", new UserChoices("one", "two") },
                };

    private void Update()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Dialog)
        {
            userChoice1.text = UserResponses[computerText.text].choice1;
            userChoice2.text = UserResponses[computerText.text].choice2;
        }
        else if (StateMachine.Instance.currentGameState == StateMachine.State.Explore)
        {
           computerText.text = ComputerResponseDictionary["Initial"];
        }
    }
}

public class UserChoices
{
    public string choice1;
    public string choice2;
    
    public UserChoices(string a, string b)
    {
        choice1 = a;
        choice2 = b;
    }
}
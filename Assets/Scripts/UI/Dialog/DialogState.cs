using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

public class DialogState : Singleton<DialogState> {

    public Dictionary<string, string> UserInputToComputerResponse = new Dictionary<string, string>()
                {
                    { "Initial", "I want a sandwich"},
                    { "How are you?", "I'm not here for conversation, I want a sandwich"},
                    { "What do you want on it?", "The User pressed the SECOND button"},
                    { "User Response 3", "The User pressed the THIRD button"},
                    { "User Response 4", "The User pressed the FOURTH button"},
                    { "Failure", "You're taking forever... Hurry up"}
                };

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

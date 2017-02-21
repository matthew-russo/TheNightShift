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

    private Text _computerResponseText;

    public void Start()
    {
        _computerResponseText = GetComponent<Text>();
        _computerResponseText.text = _userInputToComputerResponse["Initial"];
    }

    public void Respond (string UserResponse)
    {
        string computerResponse = _userInputToComputerResponse[UserResponse];
        _computerResponseText.text = computerResponse;
	}

}

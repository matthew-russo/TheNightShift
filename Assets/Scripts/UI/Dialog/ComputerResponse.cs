using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// When a sandwich is made, the customer responds according to the player's ability to follow instructions
/// </summary>

public class ComputerResponse : MonoBehaviour
{
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

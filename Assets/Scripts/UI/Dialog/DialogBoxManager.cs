using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Acivates / Deactives Dialog elements based upon Game State
/// </summary>

public class DialogBoxManager : MonoBehaviour
{
    private ComputerResponse _computerResponse;
    public GameObject Panel;
    public GameObject UserInputButton1;
    public GameObject UserInputButton2;
	
	void Update () {

	    switch (StateMachine.Instance.currentGameState)
	    {
            case StateMachine.State.Dialog:
                if (!Panel.activeInHierarchy || !UserInputButton1.activeInHierarchy || !UserInputButton2.activeInHierarchy) { 
                    Panel.SetActive(true);
                    UserInputButton1.SetActive(true);
                    UserInputButton2.SetActive(true);
                }
	            break;

            case StateMachine.State.Sandwich:
                if (UserInputButton1.activeInHierarchy || UserInputButton2.activeInHierarchy)
	            {
	                UserInputButton1.SetActive(false);
                    UserInputButton2.SetActive(false);
                }
	            break;

            case StateMachine.State.Explore:
                if (Panel.activeInHierarchy)
	            {
	                Panel.SetActive(false);
	            }
	            break;
	    }
	}
}

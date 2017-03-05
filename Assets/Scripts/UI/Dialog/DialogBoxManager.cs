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
            // When in Dialog state, show user buttons
            //
            case StateMachine.State.Dialog:
                if (!Panel.activeInHierarchy || !UserInputButton1.activeInHierarchy || !UserInputButton2.activeInHierarchy) { 
                    Panel.SetActive(true);
                    UserInputButton1.SetActive(true);
                    UserInputButton2.SetActive(true);
                }
	            break;
            
            // When in Sandwich state, show text, but no buttons
            //
            case StateMachine.State.Sandwich:
                if (UserInputButton1.activeInHierarchy || UserInputButton2.activeInHierarchy)
	            {
	                UserInputButton1.SetActive(false);
                    UserInputButton2.SetActive(false);
                }
	            break;

            // When in Explore state, show no UI element
            //
            case StateMachine.State.Explore:
                if (Panel.activeInHierarchy)
	            {
	                Panel.SetActive(false);
	            }
	            break;
	    }
	}
}

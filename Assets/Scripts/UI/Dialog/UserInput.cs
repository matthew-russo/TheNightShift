using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script placed on Buttons that take in the User Input and has the customer respond according to the DialogState Script
/// TODO: Refactor the playerChoice cases so that they have a tag rather than manually putting LOGICAL OR (||) statements
/// </summary>

public class UserInput : MonoBehaviour, IPointerClickHandler
{
    public RecipeGenerator recipeGenerator;
    public ComputerResponse computerResponse;
    public CustomerManager customerManager;
    public CustomerHappiness customerHappiness;

    public string playerChoice;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        playerChoice = transform.GetChild(0).GetComponent<Text>().text;
        
        // If player picked this choice, move to the Sandwich State, pick a recipe out of the queue, & move the camera to the sandwich
        //
        if (playerChoice == "What do you want on it?")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Sandwich;
            recipeGenerator.PickNextRecipe();
            CameraCoroutines.Instance.startCameraToSandwich = true;
            playerChoice = "";
        }

        // If player chooses any of the negative ending dialog choices, move state to Explore and decrease Customer Happiness
        //
        else if (playerChoice == "Yup" || playerChoice == "My ass..." || playerChoice == "Not here" || playerChoice == "I don't know" || playerChoice == "Yeah" || playerChoice == "Okay" || playerChoice == "Bite me")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Explore;
            customerManager._haveCustomersBeenWiped = false;
            customerHappiness._image.fillAmount -= .15f;
        }

        // If player chooses any of the positve ending dialog choices, move state to Explore and increase Customer Happiness
        //
        else if (playerChoice == "*thumbs up*" || playerChoice == "Thanks" || playerChoice == "No problemo" || playerChoice == "Have a good one" || playerChoice == "Enjoy!")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Explore;
            customerManager._haveCustomersBeenWiped = false;
            customerHappiness._image.fillAmount += .25f;
        }

        // If it is a standard dialog choice, have the customer respond according to the DialogState Script's dictionaries.
        //
        else
        {
            Debug.Log(playerChoice);
            computerResponse.computerResponseText.text = DialogState.Instance.CustomerResponseDictionary[playerChoice];
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Script placed on Buttons that take in the User Input and has the customer respond according to the DialogState Script
/// </summary>

public class UserInput : MonoBehaviour, IPointerClickHandler
{
    public RecipeGenerator recipeGenerator;
    public ComputerResponse computerResponse;
    public CustomerManager customerManager;
    public CustomerHappiness customerHappiness;

    public string playerChoice;
    public UserChoices currentChoices;

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
        else if (currentChoices.tag == "AngryExit")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Explore;
            customerManager._haveCustomersBeenWiped = false;
            customerHappiness._image.fillAmount -= .15f;
        }

        // If player chooses any of the positve ending dialog choices, move state to Explore and increase Customer Happiness
        //
        else if (currentChoices.tag == "HappyExit")
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

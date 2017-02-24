using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInput : MonoBehaviour, IPointerClickHandler
{
    public RecipeGenerator recipeGenerator;
    public DialogBoxManager dialogBoxManager;
    public ComputerResponse computerResponse;
    public CameraCoroutines CameraCoroutines;
    public CustomerManager customerManager;
    public CustomerHappiness customerHappiness;

    public string playerChoice;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
        playerChoice = transform.GetChild(0).GetComponent<Text>().text;
        if (playerChoice == "What do you want on it?")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Sandwich;
            recipeGenerator.PickNextRecipe();
            CameraCoroutines.Instance.startCameraToSandwich = true;
            playerChoice = "";
        }
        else if (playerChoice == "Yup" || playerChoice == "My ass..." || playerChoice == "Not here" || playerChoice == "I don't know" || playerChoice == "Yeah" || playerChoice == "Okay" || playerChoice == "Bite me")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Explore;
            customerManager._haveCustomersBeenWiped = false;
            customerHappiness._image.fillAmount -= .15f;
        }
        else if (playerChoice == "*thumbs up*" || playerChoice == "Thanks" || playerChoice == "No problemo" || playerChoice == "Have a good one" || playerChoice == "Enjoy!")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Explore;
            customerManager._haveCustomersBeenWiped = false;
            customerHappiness._image.fillAmount += .25f;
        }
        else
        {
            Debug.Log(playerChoice);
            computerResponse.computerResponseText.text = DialogState.Instance.ComputerResponseDictionary[playerChoice];
        }
    }
}

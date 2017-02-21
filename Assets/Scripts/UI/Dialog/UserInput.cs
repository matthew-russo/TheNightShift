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
    public DialogToSandwichMaker dialogToSandwichMaker;

    public string playerChoice;

    public void OnPointerClick(PointerEventData eventData)
    {
        playerChoice = eventData.selectedObject.transform.GetChild(0).GetComponent<Text>().text;
        if (playerChoice == "What do you want on it?")
        {
            StateMachine.Instance.currentGameState = StateMachine.State.Sandwich;
            recipeGenerator.PickNextRecipe();
            dialogToSandwichMaker.startCameraPan = true;
            playerChoice = null;
        }
        else
        {
            computerResponse.Respond(playerChoice);
        }

        PickNewWordChoice();

    }

    public void PickNewWordChoice()
    {
        
    }

}

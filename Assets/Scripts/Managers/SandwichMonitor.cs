using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

/// <summary>
/// Responsible for seeing if sandwich the player made matches what the customer asked for
/// TODO: Redo check system so that it checks as player adds individual items, give audio feedback
/// TODO: Fix counting system so that OnTriggerExit removes the object from the currentSandwich List
/// </summary>

public class SandwichMonitor : Singleton<SandwichMonitor>
{
    public float targetParts;
    public float correctCount;
    public string recipe;

    public ComputerResponse _computerResponse;
    public CustomerHappiness _customerHappiness;
    public RecipeGenerator recipeGenerator;
    public List<GameObject> currentSandwich = new List<GameObject>();

    public AudioClip correct;
    public AudioClip wrong;

    // While game is in Sandwich state, get the recipe text and number of target ingredient from the recipeGenerator
    // If the current sandwich has the same number of ingredients as the recipe, change the state to Dialog, move the camera to the customer,
    // Stop decreasing the Customer Happiness bar, send the results of the sandwich to the ComputerResponse script
    //
    void Update()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            recipe = recipeGenerator.Recipe;
            targetParts = recipeGenerator.targetParts;
            if (currentSandwich.Count == targetParts)
            {
                StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
                CameraCoroutines.Instance.startCameraToCustomer = true;
                _customerHappiness.customerUnhappy = false;
                _computerResponse.EvaluateSandwich(CheckSandwich());
            }
        }
    }

    // Function that compares the player's sandwich to the recipe
    // Returns a float representing the percentage of ingredients that the player got correct
    //
    private float CheckSandwich()
    {
        string Output = "";
        for (int i = 0; i < currentSandwich.Count; i++)
        {
            Output += currentSandwich[i];
            Output += ", ";
            if (recipe.Contains(currentSandwich[i].name))
            {
                correctCount++;
            }
            if (currentSandwich[i].GetComponent<FoodState>())
            {
                currentSandwich[i].GetComponent<FoodState>().setDown = false;
            }
            currentSandwich[i].transform.position = currentSandwich[i].GetComponent<Draggable>()._originPostion;
        }
        currentSandwich.Clear();
        float ratio = correctCount / targetParts;
        correctCount = 0;
        return ratio;
    }

    // When something enters the prep area's trigger, check if it is a food item and switch a bool so that Draggable script can determine if piece should be added to currentSandwich
    //
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Draggable>() != null)
        {
            col.GetComponent<Draggable>().inTriggerArea = true;
        }
    }

    // When something exits the prep area's trigger, check if it is a food item and switch a bool so that Draggable script can determine if piece should be added to currentSandwich
    //
    private void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Draggable>() != null)
        {
            col.GetComponent<Draggable>().inTriggerArea = false;
        }
    }
}

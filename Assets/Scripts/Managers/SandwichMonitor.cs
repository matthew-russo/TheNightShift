using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichMonitor : MonoBehaviour
{
    private int sandwichParts;
    public int targetParts;
    public int correctCount;

    public RecipeGenerator recipeGenerator;

    public string recipe;

    public List<string> currentSandwich = new List<string>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
	    {
	        recipe = recipeGenerator.Recipe;
            targetParts = recipeGenerator.targetParts;
            if (currentSandwich.Count == targetParts)
            {
                Debug.Log("Sandwich Made");
                if (CheckSandwich())
                {
                    StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
                    Debug.Log("success");
                }
                else
                {
                    StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
                    Debug.Log("failure");
                }
            }
        }
	}

    private bool CheckSandwich()
    {
        string Output = "";
        for (int i = 0; i < currentSandwich.Count; i++)
        {
            Output += currentSandwich[i];
            Output += ", ";
            if (recipe.Contains(currentSandwich[i]))
            {
                correctCount++;
            }
        }
        Debug.Log("RECIPE " + recipe);
        Debug.Log("OUTPUT " +Output);
        //foreach (string item in currentSandwich)
        //{
        //    currentSandwich.Remove(item);
        //}
        if (correctCount == targetParts)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}

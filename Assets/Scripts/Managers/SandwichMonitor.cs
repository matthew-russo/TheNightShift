using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichMonitor : MonoBehaviour
{
    private float sandwichParts;
    public float targetParts;
    public float correctCount;
    public ComputerResponse _computerResponse;
    public CustomerHappiness _customerHappiness;

    public RecipeGenerator recipeGenerator;

    public string recipe;

    public List<GameObject> currentSandwich = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            recipe = recipeGenerator.Recipe;
            targetParts = recipeGenerator.targetParts;
            if (currentSandwich.Count == targetParts)
            {
                _customerHappiness.customerUnhappy = false;
                StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
                _computerResponse.EvaluateSandwich(CheckSandwich());
                CameraCoroutines.Instance.startCameraToCustomer = true;
            }
        }
    }

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
            //Destroy(currentSandwich[i]);
            //Debug.Log(currentSandwich[i].name);
            //currentSandwich[i].GetComponent<Draggable>().ReturnToPool();
            currentSandwich[i].transform.position = currentSandwich[i].GetComponent<Draggable>()._originPostion;
        }
        currentSandwich.Clear();
        float ratio = correctCount / targetParts;
        correctCount = 0;
        return ratio;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Draggable>() != null)
        {
            col.GetComponent<Draggable>().inTriggerArea = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Draggable>() != null)
        {
            col.GetComponent<Draggable>().inTriggerArea = false;
        }
    }
}

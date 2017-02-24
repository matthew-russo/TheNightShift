using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomizationKit;
using UnityEngine.UI;

/// <summary>
/// Generates random recipes, fills a queue with them, and automatically refills queue when empty
/// Provides a public function to pick a new recipe and display it on the UI Panel;
/// </summary>

public class RecipeGenerator : MonoBehaviour
{
    public Queue<string> RecipeQueue = new Queue<string>();

    private List<string> BreadChoice = new List<string>(new string[] {"White", "Wheat"});
    private List<string> MeatChoice = new List<string>(new string[] {"Salami", "Turkey", "Ham", "Tuna Salad"});
    private List<string> CheeseChoice = new List<string>(new string[] {"Provolone", "Cheddar", "Pepper Jack", "Swiss"});
    private List<string> ToppingsChoice = new List<string>(new string[] {"Lettuce", "Tomato", "Banana Peppers", "Pickles", "Onions", "Jalepenos"});
    private List<string> DressingsChoice = new List<string>(new string[] {"Mayonaise", "Ketchup", "Mustard", "Ranch", "Buffalo"});

    public Text RecipeText;
    public string Recipe;

    public int targetParts;

    // Fills the queue at the beginning of the play session
    //
    void Awake ()
    {
        FillRecipeQueue();
    }
	
    // If the queue is empty, refill it
    //
	void Update () {
	    if (RecipeQueue.Count == 0)
	    {
	        FillRecipeQueue();
	    }
	}

    // Fills a queue with 10 recipes by Randomly shuffling the above food lists and picking items out of the shuffled list.
    //
    private void FillRecipeQueue()
    {
        for (int i = 0; i < 10; i++)
        {
            string currentRecipe = "";

            currentRecipe += (RandomFuncs.FYShuffle(BreadChoice)[0]) + ", ";
            currentRecipe += (RandomFuncs.FYShuffle(MeatChoice)[0]) + ", ";
            currentRecipe += (RandomFuncs.FYShuffle(CheeseChoice)[0]) + ", ";

            ToppingsChoice = RandomFuncs.FYShuffle(ToppingsChoice);
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                currentRecipe += ToppingsChoice[j] + ", ";
            }

            DressingsChoice = RandomFuncs.FYShuffle(DressingsChoice);
            int dressingsAmount = Random.Range(1,2);
            for (int j = 0; j < dressingsAmount; j++)
            {
                currentRecipe += DressingsChoice[j];
                if (j != dressingsAmount-1)
                {
                    currentRecipe += ", ";
                }
            }

            RecipeQueue.Enqueue(currentRecipe);
        }
    }

    // Takes a recipe out of the queue, sets the customers text to the recipe, & split it to give SandwichMonitor script the number of target parts
    //
    public void PickNextRecipe()
    {
        Recipe = RecipeQueue.Dequeue();
        RecipeText.text = "Make me a : " + Recipe;
        string[] splitRecipe = Recipe.Split(',');
        targetParts = splitRecipe.Length;
    }
}

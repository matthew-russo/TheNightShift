using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;
using RandomizationKit;
using UnityEngine.UI;

/// <summary>
/// Handles customer spawning and interaction
/// TODO: Clean this up, its a bit messy
/// </summary>

public class CustomerManager : Singleton<CustomerManager>
{
    public bool isCustomerInPosition = false;
    public bool isPlayerInPosition = false;
    public bool _haveCustomersBeenWiped = false;
    public float _timeUntilNextCustomer = 7f;
    private float _attentionTimer = 10f;

    public GameObject attentionUI;
    public GameObject customerPrefab;
    public GameObject[] _customerParent;
    public CustomerHappiness customerHappiness; 

	void Update () {
	    if (StateMachine.Instance.currentGameState == StateMachine.State.Explore)
	    {
            // If Customers haven't been wiped, destroy all objects with the "Customer" tag, and resets timer & booleans
            // This is mostly housekeeping
            //
	        //if (!_haveCustomersBeenWiped)
	        //{
	        //    GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
	        //    foreach (GameObject item in customers)
	        //    {
	        //        GameObject.Destroy(item);
	        //    }
	        //    _isCustomerActive = false;
         //       _timeUntilNextCustomer = Random.Range(7f, 30f);
         //       _haveCustomersBeenWiped = true;
	        //}

            // If the customer is spawned and player is inside the trigger by sandiwch part, move to Dialog state and move camera to face customer
            //
	        if (isCustomerInPosition && isPlayerInPosition)
	        {
                _timeUntilNextCustomer = Random.Range(7f, 30f);
                StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
	            isCustomerInPosition = false;
	            isPlayerInPosition = false;
	            CameraCoroutines.Instance.startCameraToCustomer = true;
	        }
            
            // If customer is spawned but player is not inside the trigger, spawn the attention UI after _attentionTimer time and start decreasing Customer Happiness
	        else if (isCustomerInPosition && !isPlayerInPosition)
	        {
	            if (_attentionTimer <= 0)
	            {
	                customerHappiness.DecreaseFill();
                    attentionUI.SetActive(true);
                }
	            else
	            {
	                _attentionTimer -= Time.deltaTime;
	            }
	        }
	    }

        // If not in Explore state, turn off the Attention UI Symbol
        //
	    else
	    {
	        if (attentionUI.activeInHierarchy) { 
	            attentionUI.SetActive(false);
	        }
	    }


        // Timer to spawn Customers
        //
	    if (_timeUntilNextCustomer <= 0 && !isCustomerInPosition && CustomerNavigation.customerCount <= 0)
	    {
	        SpawnCustomer();
	    }
	    else
	    {
	        _timeUntilNextCustomer -= Time.deltaTime;
	    }
	}

    // Spawns a new customer, set its parent to empty Customers Object, give it the "Customer" tag and switch a bool to keep track of whether a customer is currently spawned
    //
    private void SpawnCustomer()
    {
        RandomFuncs.FYShuffle(_customerParent);
        GameObject spawnParent = _customerParent[0];
        GameObject newCustomer = GameObject.Instantiate(customerPrefab, spawnParent.transform.position, Quaternion.identity);
        newCustomer.transform.SetParent(spawnParent.transform);
        newCustomer.transform.localPosition = spawnParent.transform.position;
        _timeUntilNextCustomer = Random.Range(5f, 30f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles customer spawning and interaction
/// TODO: Clean this up, its a bit messy
/// </summary>

public class CustomerManager : MonoBehaviour
{
    public bool _isCustomerActive = false;
    public bool _isPlayerInPosition = false;
    public bool _haveCustomersBeenWiped = false;
    public float _timeUntilNextCustomer = 7f;
    private float _attentionTimer = 10f;

    public GameObject attentionUI;
    public GameObject customerPrefab;
    private GameObject _customerParent;
    public CustomerHappiness customerHappiness;
    private Vector3 customerSpawnPos = new Vector3(-1f, 0f, 3f);

    private void Start()
    {
        _customerParent = GameObject.Find("Customers");
    }

	void Update () {
	    if (StateMachine.Instance.currentGameState == StateMachine.State.Explore)
	    {
            // If Customers haven't been wiped, destroy all objects with the "Customer" tag, and resets timer & booleans
            // This is mostly housekeeping
            //
	        if (!_haveCustomersBeenWiped)
	        {
	            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
	            foreach (GameObject item in customers)
	            {
	                GameObject.Destroy(item);
	            }
	            _isCustomerActive = false;
                _timeUntilNextCustomer = Random.Range(7f, 30f);
                _haveCustomersBeenWiped = true;
	        }

            // If the customer is spawned and player is inside the trigger by sandiwch part, move to Dialog state and move camera to face customer
            //
	        if (_isCustomerActive && _isPlayerInPosition)
	        {
                _timeUntilNextCustomer = Random.Range(7f, 30f);
                StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
	            CameraCoroutines.Instance.startCameraToCustomer = true;
	        }
            
            // If customer is spawned but player is not inside the trigger, spawn the attention UI after _attentionTimer time and start decreasing Customer Happiness
	        else if (_isCustomerActive && !_isPlayerInPosition)
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
	    if (_timeUntilNextCustomer <= 0 && !_isCustomerActive)
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
        GameObject newCustomer = GameObject.Instantiate(customerPrefab, customerSpawnPos, Quaternion.identity);
        newCustomer.transform.SetParent(_customerParent.transform);
        newCustomer.transform.localPosition = customerSpawnPos;
        newCustomer.tag = "Customer";
        _isCustomerActive = true;
    }
}

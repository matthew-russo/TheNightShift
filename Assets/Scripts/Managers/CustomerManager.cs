using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public bool _isCustomerActive = false;
    public bool _isPlayerInPosition = false;
    public float _timeUntilNextCustomer = 5f;
    public GameObject attentionUI;
    public GameObject customerPrefab;
    private GameObject _customerParent;
    public CustomerHappiness customerHappiness;

    public bool _haveCustomersBeenWiped = false;

    private float _timer = 10f;

    private Vector3 customerSpawnPos = new Vector3(-1f, 0f, 3f);

    private void Start()
    {
        _customerParent = GameObject.Find("Customers");
    }

	void Update () {
	    if (StateMachine.Instance.currentGameState == StateMachine.State.Explore)
	    {
	        if (!_haveCustomersBeenWiped)
	        {
	            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
	            foreach (GameObject item in customers)
	            {
	                GameObject.Destroy(item);
	            }
	            _isCustomerActive = false;
	            _timeUntilNextCustomer = 5f;
	            _haveCustomersBeenWiped = true;
	        }

	        if (_isCustomerActive && _isPlayerInPosition)
	        {
	            _timeUntilNextCustomer = 5f;
	            StateMachine.Instance.currentGameState = StateMachine.State.Dialog;
	            CameraCoroutines.Instance.startCameraToCustomer = true;
	        }
	        else if (_isCustomerActive && !_isPlayerInPosition)
	        {
	            if (_timer <= 0)
	            {
	                customerHappiness.DecreaseFill();
                    attentionUI.SetActive(true);
                }
	            else
	            {
	                _timer -= Time.deltaTime;
	            }
	        }
	    }
	    else
	    {
	        if (attentionUI.activeInHierarchy) { 
	            attentionUI.SetActive(false);
	        }
	    }

	    if (_timeUntilNextCustomer <= 0 && !_isCustomerActive)
	    {
	        SpawnCustomer();
	    }
	    else
	    {
	        _timeUntilNextCustomer -= Time.deltaTime;
	    }
	}

    private void SpawnCustomer()
    {
        GameObject newCustomer = GameObject.Instantiate(customerPrefab, customerSpawnPos, Quaternion.identity);
        newCustomer.transform.SetParent(_customerParent.transform);
        newCustomer.transform.localPosition = customerSpawnPos;
        newCustomer.tag = "Customer";
        //attentionUI.SetActive(true);
        _isCustomerActive = true;
    }
}

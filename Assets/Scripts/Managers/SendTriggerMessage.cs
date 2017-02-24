using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTriggerMessage : MonoBehaviour
{
    private CustomerManager customerManager;

    private void Start()
    {
        customerManager = transform.parent.gameObject.GetComponent<CustomerManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            customerManager._isPlayerInPosition = true;
        }
        if (col.gameObject.tag == "Customer")
        {
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            customerManager._isPlayerInPosition = false;
        }
        if (col.gameObject.tag == "Customer")
        {

        }
    }
}

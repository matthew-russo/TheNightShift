using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors whether customer and player are in position and lets the Customer Manager know accordingly.
/// </summary>

public class SendTriggerMessage : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CustomerManager.Instance.isPlayerInPosition = true;
        }
        if (col.gameObject.tag == "Customer")
        {
            CustomerManager.Instance.isCustomerInPosition = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CustomerManager.Instance.isPlayerInPosition = false;
        }
        if (col.gameObject.tag == "Customer")
        {
            CustomerManager.Instance.isCustomerInPosition = false;
        }
    }
}

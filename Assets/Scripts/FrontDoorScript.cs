using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorScript : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    public Vector3 originPosition1;
    public Vector3 openPosition1;

    public Vector3 originPosition2;
    public Vector3 openPosition2;

    private int enteredTrigger;

    // If there is a collider within the door trigger area, the door opens, if not, it closes.
    private void Update()
    {
        if (enteredTrigger > 0)
        {
            Door1.transform.localPosition = Vector3.Lerp(Door1.transform.localPosition, openPosition1, .1f);
            Door2.transform.localPosition = Vector3.Lerp(Door2.transform.localPosition, openPosition2, .1f);
        }
        else
        {
            Door1.transform.localPosition = Vector3.Lerp(Door1.transform.localPosition, originPosition1, .1f);
            Door2.transform.localPosition = Vector3.Lerp(Door2.transform.localPosition, originPosition2, .1f);
        }
    }
    
    void OnTriggerEnter()
    {
        enteredTrigger++;
    }

    void OnTriggerExit()
    {
        enteredTrigger--;
    }
}

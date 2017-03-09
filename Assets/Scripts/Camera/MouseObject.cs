using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObject : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Ray _ray;

    void Start () {
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    }
	
	void Update () {
        RaycastHit hit;
        _screenPoint = Input.mousePosition;
        _ray = Camera.main.ScreenPointToRay(_screenPoint);
        Physics.Raycast(_ray, out hit);

        transform.position = new Vector3(hit.point.x, 3f, hit.point.z);

	    if (StateMachine.Instance.currentGameState == StateMachine.State.Dialog)
	    {
	        Cursor.visible = true;
	    }
	    else
	    {
	        Cursor.visible = true;
	    }
    }
}

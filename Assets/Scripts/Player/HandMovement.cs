using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject rightHandMeshObject;
    public Animator rightAnimator;
    private SkinnedMeshRenderer _rightHandRenderer;
    private Vector3 _screenPoint;
    private bool areHandsVisible = true;
    private Ray _ray;


    void Start ()
    {
        _rightHandRenderer = rightHandMeshObject.GetComponent<SkinnedMeshRenderer>();
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    }
	
	void Update () {

        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            RaycastHit hit;
            _screenPoint = Input.mousePosition;
            _ray = Camera.main.ScreenPointToRay(_screenPoint);
            Physics.Raycast(_ray, out hit);

            Vector3 target = new Vector3(hit.point.x, SandwichMonitor.Instance.transform.position.y + .4f, hit.point.z);
            rightHand.transform.position = target;
            rightHand.transform.localPosition = new Vector3(rightHand.transform.localPosition.x, rightHand.transform.localPosition.y, rightHand.transform.localPosition.z -.07f);

            if (Input.GetMouseButtonDown(0))
                {
                    rightAnimator.SetTrigger("Grab");
                    rightAnimator.ResetTrigger("LetGo");
                }
                if (Input.GetMouseButtonUp(0))
                {
                    rightAnimator.SetTrigger("LetGo");
                    rightAnimator.ResetTrigger("Grab");
                }

            if (!areHandsVisible)
	        {
	            _rightHandRenderer.enabled = true;
	            areHandsVisible = true;
	        }
	    }
	    else
	    {
	        if (areHandsVisible)
	        {
	            _rightHandRenderer.enabled = false;
	            areHandsVisible = false;
	        }
	    }
	}
}

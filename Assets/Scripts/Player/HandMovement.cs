using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftHandMeshObject;
    public GameObject rightHandMeshObject;
    public Animator leftAnimator;
    public Animator rightAnimator;
    private SkinnedMeshRenderer _leftHandRenderer;
    private SkinnedMeshRenderer _rightHandRenderer;
    private Vector3 _screenPoint;
    private Vector3 _leftOrigin;
    private Vector3 _rightOrigin;
    private bool areHandsVisible = true;
    private Ray _ray;


    void Start ()
    {
        _leftHandRenderer = leftHandMeshObject.GetComponent<SkinnedMeshRenderer>();
        _rightHandRenderer = rightHandMeshObject.GetComponent<SkinnedMeshRenderer>();
        _leftOrigin = leftHandMeshObject.transform.position;
        _rightOrigin = rightHandMeshObject.transform.position;
        _ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    }
	
	void Update () {

        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            RaycastHit hit;
            _screenPoint = Input.mousePosition;
            _ray = Camera.main.ScreenPointToRay(_screenPoint);
            Physics.Raycast(_ray, out hit);

	        if (_screenPoint.x < Screen.width/2f)
	        {
                leftHand.transform.position = new Vector3(hit.point.x, hit.point.y + .3f, hit.point.z);
                leftHand.transform.localPosition = new Vector3(leftHand.transform.localPosition.x, leftHand.transform.localPosition.y, leftHand.transform.localPosition.z -.1f);
                if (Input.GetMouseButtonDown(0))
                {
                    leftAnimator.SetTrigger("Grab");
                    leftAnimator.ResetTrigger("LetGo");
                }
                if (Input.GetMouseButtonUp(0))
                {
                    leftAnimator.SetTrigger("LetGo");
                    leftAnimator.ResetTrigger("Grab");
                }
            }
	        else
	        {
                rightHand.transform.position = new Vector3(hit.point.x, hit.point.y + .3f, hit.point.z);
                rightHand.transform.localPosition = new Vector3(rightHand.transform.localPosition.x, rightHand.transform.localPosition.y, rightHand.transform.localPosition.z - .1f);
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
            }


            if (!areHandsVisible)
	        {
	            _leftHandRenderer.enabled = true;
	            _rightHandRenderer.enabled = true;
	            areHandsVisible = true;
	        }
	    }
	    else
	    {
	        if (areHandsVisible)
	        {
	            _leftHandRenderer.enabled = false;
	            _rightHandRenderer.enabled = false;
	            areHandsVisible = false;
	        }
	    }
	}
}

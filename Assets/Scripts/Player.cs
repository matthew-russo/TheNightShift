using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private MouseLook _mouseLook;
    private ControllerMoveDemo _controllerMoveDemo;

    public CustomerManager customerManager;

    private Vector3 _sandwichPosition = new Vector3(26f,.5f,2.5f);

	void Start ()
	{
	    _mouseLook = GetComponentInChildren<MouseLook>();
	    _controllerMoveDemo = GetComponent<ControllerMoveDemo>();
	}
	
	void Update () {
        switch (StateMachine.Instance.currentGameState)
        {
            case StateMachine.State.Dialog:
                _mouseLook.enabled = false;
                _controllerMoveDemo.enabled = false;
                break;

            case StateMachine.State.Sandwich:
                _mouseLook.enabled = false;
                _controllerMoveDemo.enabled = false;
                StartCoroutine(MoveToSandwichPosition());
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 315f, transform.eulerAngles.z);
                break;

            case StateMachine.State.Explore:
                _mouseLook.enabled = true;
                _controllerMoveDemo.enabled = true;
                break;
        }



	    if (Input.GetKeyDown(KeyCode.R))
	    {
	        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	    }
    }

    void OnTriggerEnter(Collider col)
    {
        
    }

    private IEnumerator MoveToSandwichPosition()
    {
        while (Vector3.Distance(transform.position, _sandwichPosition) > .3f)
        {
            transform.position = Vector3.Lerp(transform.position, _sandwichPosition, .2f);
            yield return new WaitForSecondsRealtime(.1f);
        }
    }
}

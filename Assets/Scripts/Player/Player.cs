using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Core Player script that enables / disables MouseLook & ControllerMove Scripts
/// Moves player to Sandwich Station
/// DEBUG: Press R to Reset Scene
/// </summary>

public class Player : MonoBehaviour
{
    private MouseLook _mouseLook;
    private ControllerMove _controllerMove;

    public CustomerManager customerManager;

    private Vector3 _sandwichPosition = new Vector3(26f,.5f,2.5f);

	void Start ()
	{
	    _mouseLook = GetComponentInChildren<MouseLook>();
	    _controllerMove = GetComponent<ControllerMove>();
	}
	
	void Update () {
        switch (StateMachine.Instance.currentGameState)
        {
            case StateMachine.State.Dialog:
                _mouseLook.enabled = false;
                _controllerMove.enabled = false;
                break;

            case StateMachine.State.Sandwich:
                _mouseLook.enabled = false;
                _controllerMove.enabled = false;
                StartCoroutine(MoveToSandwichPosition());
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 315f, transform.eulerAngles.z);
                break;

            case StateMachine.State.Explore:
                _mouseLook.enabled = true;
                _controllerMove.enabled = true;
                break;
        }



	    if (Input.GetKeyDown(KeyCode.R))
	    {
	        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	    }
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

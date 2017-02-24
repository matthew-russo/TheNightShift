using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text _text;

	void Start ()
	{
	    _text = GetComponent<Text>();
	    _text.enabled = false;
	}
	
	void Update () {
	    if (StateMachine.Instance.currentGameState == StateMachine.State.GameOver)
	    {
	        _text.enabled = true;
	    }
	}
}

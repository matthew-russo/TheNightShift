using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public CustomerHappiness customerHappiness;

    private float flashTimer;

    private float _timer = 10f;

    private void Awake()
    {
        timerText = GetComponent<Text>();
    }

	void Update () {
	    if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
	    {
	        if (_timer > 0)
	        {
                _timer -= Time.deltaTime;
            }
	        timerText.text = "Time Left : " + _timer.ToString("F1");
	        if (_timer <= 0)
	        {
	            FlashText();
	            customerHappiness.customerUnhappy = true;
	        }
	    }
	}

    private void FlashText()
    {
        //blah
    }
}

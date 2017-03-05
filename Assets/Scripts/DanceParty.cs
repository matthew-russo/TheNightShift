using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceParty : MonoBehaviour
{

    private Light _light;

	void Start ()
	{
	    _light = GetComponent<Light>();
	}
	
	void Update ()
	{
	    Color oldColor = _light.color;
	    _light.color = Color.Lerp(oldColor, Random.ColorHSV(0f,255f,150f,255f), .2f);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerHappiness : MonoBehaviour
{
    private Image _image;
    [NonSerialized]
    public bool customerUnhappy = false;

	void Start ()
	{
	    _image = GetComponent<Image>();
	}
	
	void Update () {
	    if (_image.fillAmount > .6f)
	    {
	        _image.color = Color.green;
	    }
        else if (_image.fillAmount < .6f && _image.fillAmount > .3f)
	    {
	        _image.color = Color.yellow;
	    }
	    else
	    {
	        _image.color = Color.red;
	    }

	    if (customerUnhappy)
	    {
	        DecreaseFill();
	    }
	}

    private void DecreaseFill()
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= .001f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour
{
    private Image _image;

	void Awake ()
	{
	    _image = GetComponent<Image>();
        InvokeRepeating("Flash",.7f,.7f);
	}

    void Flash()
    {
        _image.enabled = !_image.enabled;
    }
}

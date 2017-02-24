using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

/// <summary>
/// Script Responsible for camera transitions between game states.
/// </summary>

public class CameraCoroutines : Singleton<CameraCoroutines>
{
    public GameObject Panel;
    private RectTransform _panelRectTransform;
    private GameObject _compTextObject;
    private RectTransform _compTextRectTransform;
    [NonSerialized]
    public bool startCameraToSandwich = false;

    public bool startCameraToCustomer = false;

    private void Start()
    {
        _panelRectTransform = Panel.GetComponent<RectTransform>();
        _compTextObject = Panel.transform.GetChild(0).gameObject;
        _compTextRectTransform = _compTextObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (startCameraToSandwich)
        {
            CameraToSandwich();
            startCameraToSandwich = false;
        }
        if (startCameraToCustomer)
        {
            CameraToCustomer();
            startCameraToCustomer = false;
        }
    }

    // Function that starts the three coroutines responsible for moving the camera to focus on the sandiwch.
    //
    public void CameraToSandwich()
    {
        StartCoroutine(CameraPanToSandwich());
        StartCoroutine(DialogPanUpwards());
        StartCoroutine(TextCenter());
    }

    public IEnumerator CameraPanToSandwich()
    {
        while(transform.rotation.eulerAngles.x < 58f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(transform.rotation.eulerAngles.x, 60f, .1f), -45f, 0f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator DialogPanUpwards()
    {
        while (_panelRectTransform.anchoredPosition.y < -77f)
        {
            _panelRectTransform.anchoredPosition = new Vector2(0f,Mathf.Lerp(_panelRectTransform.anchoredPosition.y, -75f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator TextCenter()
    {
        while (_compTextRectTransform.anchoredPosition.y > 2)
        {
            _compTextRectTransform.anchoredPosition = new Vector2(0f, Mathf.Lerp(_compTextRectTransform.anchoredPosition.y, 0f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    // Function that starts the three coroutines responsible for moving the camera to focus on the customer.
    //
    public void CameraToCustomer()
    {
        StartCoroutine(CameraPanToCustomer());
        StartCoroutine(DialogPanDownwards());
        StartCoroutine(TextMoveUp());
    }

    public IEnumerator CameraPanToCustomer()
    {
        while (transform.rotation.eulerAngles.x > 12f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(transform.rotation.eulerAngles.x, 10f, .1f), -45f, 0f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator DialogPanDownwards()
    {
        while (_panelRectTransform.anchoredPosition.y > -213)
        {
            _panelRectTransform.anchoredPosition = new Vector2(0f, Mathf.Lerp(_panelRectTransform.anchoredPosition.y, -215f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator TextMoveUp()
    {
        while (_compTextRectTransform.anchoredPosition.y < 33)
        {
            _compTextRectTransform.anchoredPosition = new Vector2(0f, Mathf.Lerp(_compTextRectTransform.anchoredPosition.y, 35f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }


}

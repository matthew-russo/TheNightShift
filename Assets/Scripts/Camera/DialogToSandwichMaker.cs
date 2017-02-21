using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogToSandwichMaker : MonoBehaviour
{
    public GameObject Panel;
    private RectTransform _panelRectTransform;
    private GameObject _compTextObject;
    private RectTransform _compTextRectTransform;
    [NonSerialized]
    public bool startCameraPan = false;

    private void Start()
    {
        _panelRectTransform = Panel.GetComponent<RectTransform>();
        _compTextObject = Panel.transform.GetChild(0).gameObject;
        _compTextRectTransform = _compTextObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (startCameraPan)
        {
            StartCoroutine(CameraPanToSandwich());
            StartCoroutine(DialogPanUpwards());
            StartCoroutine(TextCenter());
            startCameraPan = false;
        }
    }

    public IEnumerator CameraPanToSandwich()
    {
        while(transform.rotation.eulerAngles.x < 60f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(transform.rotation.eulerAngles.x, 60f, .1f), -45f, 0f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator DialogPanUpwards()
    {
        while (_panelRectTransform.anchoredPosition.y < -75f)
        {
            _panelRectTransform.anchoredPosition = new Vector2(0f,Mathf.Lerp(_panelRectTransform.anchoredPosition.y, -75f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }
    public IEnumerator TextCenter()
    {
        while (_compTextRectTransform.anchoredPosition.y > 0)
        {
            _compTextRectTransform.anchoredPosition = new Vector2(0f, Mathf.Lerp(_compTextRectTransform.anchoredPosition.y, 0f, .1f));
            yield return new WaitForSecondsRealtime(.01f);
        }
    }

}

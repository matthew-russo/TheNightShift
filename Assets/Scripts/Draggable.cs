using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]

public class Draggable : PooledObject
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float yPosition;

    public GameObject target;
    private BoxCollider _targetBoxCollider;
    private SandwichMonitor _sandwichMonitor;
    private bool pickedUp = false;

    [NonSerialized]
    public bool inTriggerArea = false;

    private Vector3 _originRotation;
    public Vector3 _originPostion;

    private void Start()
    {
        //GetPooledInstance<Draggable>();
        _originRotation = transform.eulerAngles;
        _originPostion = transform.position;
        _targetBoxCollider = target.GetComponent<BoxCollider>();
        _sandwichMonitor = target.GetComponent<SandwichMonitor>();
    }

    private void Update()
    {
        transform.eulerAngles = _originRotation;
    }

    
    // Allows objects to be dragged around sceen, following the mouse
    //
    void OnMouseDown()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            yPosition = target.transform.position.y + (_sandwichMonitor.currentSandwich.Count * .05f) + .4f;
            pickedUp = true;
            GetComponent<Collider>().isTrigger = true;
        }
    }

    void OnMouseDrag()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(curPosition.x, yPosition, curPosition.z);
        }
    }

    void OnMouseUp()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            GetComponent<Collider>().isTrigger = false;
            if (inTriggerArea)
            {
                _sandwichMonitor.currentSandwich.Add(gameObject);
                // TODO: CHECK SANDIWCH PART
                    // IF GOOD PLAY GOOD SOUND
                    // IF BAD SAD
                if (gameObject.name == "White" || gameObject.name == "Wheat")
                {
                    GetComponent<FoodState>().setDown = true;
                }
                this.enabled = false;
            }
        }
    }
}
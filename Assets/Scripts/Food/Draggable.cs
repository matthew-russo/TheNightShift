using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Script that allows Objects to be picked up and moved via Mouse Input
/// TODO: CHECK ITEMS AS THEY GET SET IN TARGET AREA AND PLAY AUDIO FEEDBACK BASED UPON THAT
/// </summary>

[RequireComponent(typeof(Collider))]
public class Draggable : PooledObject
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float yPosition;

    public GameObject target;
    private SandwichMonitor _sandwichMonitor;
    private bool pickedUp = false;

    [NonSerialized]
    public bool inTriggerArea = false;

    private Vector3 _originRotation;
    public Vector3 _originPostion;

    private void Start()
    {
        _originRotation = transform.eulerAngles;
        _originPostion = transform.position;
        _sandwichMonitor = target.GetComponent<SandwichMonitor>();
    }


    private void Update()
    {
        transform.eulerAngles = _originRotation;
    }

    
    // If a player clicks on a food item, set proper positions to move item, switch a bool, and turn its collider to a trigger so it doesn't knock everything around
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

    // Updates a held food item's position with the mouse position
    //
    void OnMouseDrag()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(curPosition.x, yPosition, curPosition.z);
        }
    }

    // Checks if the item is inside the prep area when mouse is released.
    // If so, adds that item to the currentSandwich List, if its bread, changes mesh to open shape, disables any further dragging
    //
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
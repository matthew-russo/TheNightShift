using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Script that allows Objects to be picked up and moved via Mouse Input
/// </summary>

[RequireComponent(typeof(Collider))]
public class Draggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float yPosition;

    private GameObject _target;
    private bool pickedUp = false;
    private bool _hasntMovedYet = true;

    [NonSerialized]
    public bool inTriggerArea = false;

    private Vector3 _originRotation;
    public Vector3 _originPostion;
    private Vector3 _originScale;
    private AudioSource _audioSource;

    private GameObject _mouseObject;

    private GameObject _sandwichParent;
    public string prefabSize;
    private GameObject prefab;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Target");
        _originRotation = transform.eulerAngles;
        _originPostion = transform.position;
        _originScale = transform.localScale;
        _audioSource = SandwichMonitor.Instance.GetComponent<AudioSource>();
        _mouseObject = GameObject.FindGameObjectWithTag("Mouse");
        _sandwichParent = GameObject.FindGameObjectWithTag("SandwichParent");

        prefab = Resources.Load(prefabSize) as GameObject;
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
            yPosition = _target.transform.position.y + (SandwichMonitor.Instance.currentSandwich.Count * .03f) +.3f;
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
            if (Vector3.Distance(transform.position, _mouseObject.transform.position) > .5f)
            {
                transform.position = Vector3.Lerp(transform.position, _mouseObject.transform.position, .6f);
                transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            }
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
                SandwichMonitor.Instance.currentSandwich.Add(gameObject);
                if (SandwichMonitor.Instance.recipe.Contains(gameObject.name))
                {
                    _audioSource.PlayOneShot(SandwichMonitor.Instance.correct);
                }
                else
                {
                    _audioSource.PlayOneShot(SandwichMonitor.Instance.wrong);
                }
                if (gameObject.name == "White" || gameObject.name == "Wheat")
                {
                    GetComponent<MeshFilter>().mesh = GetComponent<FoodState>().afterMesh;
                    GetComponent<MeshRenderer>().material = GetComponent<FoodState>().afterMaterial;
                }
                SetSandwichUp();
                transform.localPosition = _originPostion;
                transform.localEulerAngles = _originRotation;

                //this.enabled = false;
            }
        }
    }

    void SetSandwichUp()
    {
        GameObject sandwichPart = Instantiate(prefab);
        sandwichPart.transform.SetParent(_sandwichParent.transform);
        sandwichPart.transform.localPosition = new Vector3(sandwichPart.transform.position.x, sandwichPart.transform.position.y + (SandwichMonitor.Instance.currentSandwich.Count * .02f) +.15f);
        MeshFilter[] meshFilterChild = sandwichPart.GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRendererChild = sandwichPart.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshFilter item in meshFilterChild)
        {
            item.mesh = GetComponent<MeshFilter>().mesh;
            item.transform.localScale = _originScale;
            item.transform.localEulerAngles = transform.localEulerAngles;
        }
        foreach (MeshRenderer item in meshRendererChild)
        {
            item.material = GetComponent<MeshRenderer>().material;
        }
    }
}
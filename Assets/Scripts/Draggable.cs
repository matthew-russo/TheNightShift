using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]

public class Draggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float yPosition;

    public GameObject target;
    private BoxCollider _targetBoxCollider;
    private SandwichMonitor _sandwichMonitor;

    private void Start()
    {
        _targetBoxCollider = target.GetComponent<BoxCollider>();
        _sandwichMonitor = target.GetComponent<SandwichMonitor>();
    }

    // Allows objects to be dragged around sceen, following the mouse
    //
    void OnMouseDown()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            yPosition = target.transform.position.y + (_sandwichMonitor.currentSandwich.Count * .01f) + .2f;
            Debug.Log(gameObject.name + " picked up");
        }
    }

    void OnMouseDrag()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = new Vector3(curPosition.x, yPosition, curPosition.z);
            //transform.position = curScreenPoint;
        }
    }


    void OnMouseUp()
    {
        if (StateMachine.Instance.currentGameState == StateMachine.State.Sandwich)
        {
            if (Vector3.Distance(transform.position, _targetBoxCollider.center) > _targetBoxCollider.bounds.extents.x)
            {
                _sandwichMonitor.currentSandwich.Add(gameObject.name);
                if (gameObject.name == "White" || gameObject.name == "Wheat")
                {
                    GetComponent<FoodState>().setDown = true;
                }
                Destroy(this);

            }
            //if (transform.position.x > _targetBoxCollider.bounds.center.x - _targetBoxCollider.bounds.extents.x
            //    && transform.position.x < _targetBoxCollider.bounds.center.x + _targetBoxCollider.bounds.extents.x
            //    && transform.position.z > _targetBoxCollider.bounds.center.z - _targetBoxCollider.bounds.extents.z
            //    && transform.position.z < _targetBoxCollider.bounds.center.z + _targetBoxCollider.bounds.extents.z)
            //{
            //    transform.localScale = new Vector3(transform.localScale.x * _targetBoxCollider.transform.localScale.x, transform.localScale.x * _targetBoxCollider.transform.localScale.x, transform.localScale.x * _targetBoxCollider.transform.localScale.x);
            //    this.transform.SetParent(_targetBoxCollider.gameObject.transform);
            //    GetComponent<FoodState>().setDown = true;
            //    Destroy(this);
            //}
        }
        
    }
}
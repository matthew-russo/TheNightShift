using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRaycast : MonoBehaviour
{
    private Collider _currentlyHeldCollider;

    public LayerMask myRaycastMask;

    private void Update()
    {
        // 1. Construct a Ray based on the way the camera is looking
        //
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // 2. Reserve some space in memory to remember what we hit
        //
        RaycastHit rayHit = new RaycastHit();

        // 3. Shoot our raycast
        //
        if (Physics.Raycast(ray, out rayHit, 10f, myRaycastMask))
        {
            // 4. Did player Click?
            //
            if (Input.GetMouseButton(0))
            {
                _currentlyHeldCollider = rayHit.collider;
                _currentlyHeldCollider.transform.SetParent(Camera.main.transform);
            }
            if (Input.GetMouseButton(0) == false && _currentlyHeldCollider != null)
            {
                _currentlyHeldCollider.transform.parent = null;
                _currentlyHeldCollider = null;
            }
        }
    }
}

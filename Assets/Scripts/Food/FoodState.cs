using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors whether the food is set on the prep area and changes the mesh if so
/// </summary>

public class FoodState : MonoBehaviour
{
    public bool setDown;

    public Mesh beforeMesh;
    public Mesh afterMesh;
    private MeshFilter _meshFilter;

	void Start ()
	{
	    _meshFilter = GetComponent<MeshFilter>();
	}
	
	void Update ()
	{
	    if (setDown)
	    {
	        _meshFilter.mesh = afterMesh;
	    }
	    else
	    {
	        _meshFilter.mesh = beforeMesh;
	    }
	}
}

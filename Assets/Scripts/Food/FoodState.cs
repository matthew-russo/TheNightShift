using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors whether the food is set on the prep area and changes the mesh if so
/// </summary>

public class FoodState : MonoBehaviour
{
    public bool setDown;

    public Mesh afterMesh;
    public Material afterMaterial;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

	void Start ()
	{
	    _meshFilter = GetComponent<MeshFilter>();
	    _meshRenderer = GetComponent<MeshRenderer>();
	}
	
	void Update ()
	{
	    if (setDown)
	    {
	        _meshFilter.mesh = afterMesh;
	        _meshRenderer.material = afterMaterial;
	    }
	}
}

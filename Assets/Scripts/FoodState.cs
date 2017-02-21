using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	        setDown = false;
	    }
	}
}

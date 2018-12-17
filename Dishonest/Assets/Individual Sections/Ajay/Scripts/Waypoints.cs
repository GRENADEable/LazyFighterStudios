using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

	public float drawRadius = 1.0f;

	public virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, drawRadius);
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
}

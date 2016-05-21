﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;
	
	public const float skinWidth = .015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount =4;
	
	protected float horizontalRaySpacing;
	protected float verticalRaySpacing;
	
	protected BoxCollider2D bc2d;
	public RaycastOrigins raycastOrigins;

	public virtual void Start () {
		
		bc2d = GetComponent <BoxCollider2D> ();
		CalculateRaySpacing ();
	}


	protected void UpdateRaycastOrigins () {
		
		Bounds bounds = bc2d.bounds;
		bounds.Expand (skinWidth * -2);
		
		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}
	
	protected void CalculateRaySpacing() {
		
		Bounds bounds = bc2d.bounds;
		bounds.Expand (skinWidth * -2);
		
		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}
	
	//Struct to store positions of corners of our player object
	
	public struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeLineRenderer : MonoBehaviour
{
	public Transform ropeStart; // Assign the start of the rope or the root
	private LineRenderer lineRenderer;
	private List<Vector3> ropePositions = new List<Vector3>();

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		UpdateRopePositions();
	}

	void UpdateRopePositions()
	{
		ropePositions.Clear();

		// This assumes all children are part of the rope. If there are other children, adjust accordingly.
		foreach(Transform child in ropeStart) {
			ropePositions.Add(child.position);
		}

		lineRenderer.positionCount = ropePositions.Count;
		lineRenderer.SetPositions(ropePositions.ToArray());
	}
}

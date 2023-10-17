using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadAlignmentChecker : MonoBehaviour
{
	public LineRenderer userLine;
	public LineRenderer worldLine;
	public Material alignedMaterial;
	public Material notAlignedMaterial;
	public Transform head;

	void Update()
	{
		userLine.transform.rotation = head.rotation;

		float angleDifference = Quaternion.Angle(worldLine.transform.rotation, userLine.transform.rotation);

		// If the angle difference is less than a certain threshold, they are aligned
		if(angleDifference < 5f) // Change this threshold value as needed
		{
			userLine.material = alignedMaterial;
			worldLine.material = alignedMaterial;
		}
		else {
			userLine.material = notAlignedMaterial;
			worldLine.material = notAlignedMaterial;
		}
	}
}

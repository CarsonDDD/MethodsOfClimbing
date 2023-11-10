using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCenterFinder : MonoBehaviour
{
	public Transform headTransform;
	public Transform leftHandTransform;
	public Transform rightHandTransform;

	public float heightOffset = -0.345f;

	void Update()
	{
		Vector3 averageHandPosition = (leftHandTransform.position + rightHandTransform.position) / 2;
		transform.position = new Vector3(
			averageHandPosition.x,
			headTransform.position.y + heightOffset,
			averageHandPosition.z
		);

		// proper rotation, may not be wanted?
		Vector3 forwardDirection = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;
		transform.forward = forwardDirection;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarnessController : MonoBehaviour
{
    public Transform headTransform;

	public float heightOffset = -0.345f; // Since we are locking the transform to the head in update, we need a seperate way to controll the y offset.

	private Vector3 startingPosOffset;
	private Quaternion startingRotationOffset;

	// Start is called before the first frame update
	void Start()
    {
		startingPosOffset = transform.position - headTransform.position;
		startingRotationOffset = Quaternion.Inverse(headTransform.rotation) * transform.rotation;
	}

    // Update is called once per frame
    void Update()
    {
		// transform keeping original offsets
		Vector3 targetPosition = headTransform.position + startingPosOffset;
		Quaternion targetRotation = headTransform.rotation * startingRotationOffset;

		// lock rotation to only y
		targetRotation.x = 0;
		targetRotation.z = 0;

		// apply our seperate y adjustment
		targetPosition.y = headTransform.position.y - heightOffset;

		transform.position = targetPosition;
		transform.rotation = targetRotation;
	}
}

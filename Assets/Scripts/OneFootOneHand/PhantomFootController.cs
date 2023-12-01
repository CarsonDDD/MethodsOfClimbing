using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomFootController : MonoBehaviour
{
	public enum PhantomFootImplmentation
	{
		HandBased,
		DirectMirror,
		InverseMirror
	}

	public PhantomFootImplmentation trackingMethod = PhantomFootImplmentation.HandBased;

	public FootController realFoot;
	public Transform leftHandController;
	public Transform bodyCenter;// not nessesarily camera. Has harness issue
	public GameObject phantomFootPrefab;

	private GameObject phantomFootInstance;

	public float maxDistanceX = 0.05f;
	public float maxDistanceZ = 0.075f;


	void Start()
	{
		phantomFootInstance = Instantiate(phantomFootPrefab, bodyCenter);
	}

	void Update()
	{
		// Only show if real foot is showing
		if(!realFoot.gameObject.activeSelf) {
			if(phantomFootInstance.activeSelf) {
				phantomFootInstance.SetActive(false);
			}
			return;
		}


		if(!phantomFootInstance.activeSelf) {
			phantomFootInstance.SetActive(true);
		}


		switch(trackingMethod) {
			case PhantomFootImplmentation.HandBased:
				HandBasedTracking();
			break;
			case PhantomFootImplmentation.DirectMirror: 
				DirectMirrorTracking();
			break;
			case PhantomFootImplmentation.InverseMirror:
				InverseMirrorTracking();
			break;
		}
	}

	// used for in editor access
	public void SwitchTrackingMethod(PhantomFootImplmentation newMethod)
	{
		trackingMethod = newMethod;
	}

	public void SwitchToHandBased() => trackingMethod = PhantomFootImplmentation.HandBased;

	public void SwitchToMirrorBased() => trackingMethod = PhantomFootImplmentation.DirectMirror;

	public void SwitchToInverseMirrorBased() => trackingMethod = PhantomFootImplmentation.InverseMirror;

	private void HandBasedTracking()
	{
		Vector3 desiredPosition = new Vector3(leftHandController.position.x, realFoot.transform.position.y, leftHandController.position.z);

		Vector3 relativePosition = desiredPosition - bodyCenter.position;

		// Clamp the position to be within the threshold. 
		relativePosition.x = Mathf.Clamp(relativePosition.x, -maxDistanceX, maxDistanceX);
		relativePosition.z = Mathf.Clamp(relativePosition.z, -maxDistanceZ, maxDistanceZ);


		phantomFootInstance.transform.position = bodyCenter.position + relativePosition;

		// match rotation of tracked foot
		phantomFootInstance.transform.rotation = Quaternion.Euler(0, leftHandController.eulerAngles.y, 0);
	}

	private void DirectMirrorTracking()
	{
		Vector3 localPosition = bodyCenter.InverseTransformPoint(realFoot.transform.position);

		localPosition.x = -localPosition.x;// mirror

		Vector3 mirroredPosition = bodyCenter.TransformPoint(localPosition);

		phantomFootInstance.transform.position = mirroredPosition;
		phantomFootInstance.transform.rotation = Quaternion.Euler(0f, realFoot.transform.eulerAngles.y, -realFoot.transform.eulerAngles.z);
	}

	// I forget what this was for
	/*private bool IsFootForward(Transform footTransform, Transform bodyCenter)
	{
		return Vector3.Dot((footTransform.position-bodyCenter.position).normalized, bodyCenter.forward) >0;
	}*/

	private void InverseMirrorTracking()
	{
		Vector3 localPosition = bodyCenter.InverseTransformPoint(realFoot.transform.position);

		localPosition.z = -localPosition.z;// font back.

		localPosition.x = -localPosition.x;// side (normal mirror)

		Vector3 insverseWorld = bodyCenter.TransformPoint(localPosition);

		phantomFootInstance.transform.position = insverseWorld;
		phantomFootInstance.transform.rotation = Quaternion.Euler(0f, realFoot.transform.eulerAngles.y, -realFoot.transform.eulerAngles.z);
	}

}

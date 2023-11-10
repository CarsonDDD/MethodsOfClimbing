using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomFootController : MonoBehaviour
{
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
		if(realFoot.gameObject.activeSelf) {
			if(!phantomFootInstance.activeSelf) {
				phantomFootInstance.SetActive(true);
			}

			// mirror
			/*Vector3 mirroredPosition = MirrorPosition(realFoot.transform.position);
			phantomFootInstance.transform.position = mirroredPosition;
			phantomFootInstance.transform.rotation = Quaternion.Euler(0f, realFoot.transform.eulerAngles.y, -realFoot.transform.eulerAngles.z);*/

			
			Vector3 desiredPosition = new Vector3(leftHandController.position.x,realFoot.transform.position.y,leftHandController.position.z);

			Vector3 relativePosition = desiredPosition - bodyCenter.position;

			// Clamp the position to be within the threshold. 
			relativePosition.x = Mathf.Clamp(relativePosition.x, -maxDistanceX, maxDistanceX);
			relativePosition.z = Mathf.Clamp(relativePosition.z, -maxDistanceZ, maxDistanceZ);

			
			phantomFootInstance.transform.position = bodyCenter.position + relativePosition;

			// match rotation of tracked foot
			phantomFootInstance.transform.rotation = Quaternion.Euler(0,leftHandController.eulerAngles.y,0);
		}
		else {
			if(phantomFootInstance.activeSelf) {
				phantomFootInstance.SetActive(false);
			}
		}
	}

	private bool IsFootForward(Transform footTransform, Transform bodyCenter)
	{
		return Vector3.Dot((footTransform.position-bodyCenter.position).normalized, bodyCenter.forward) >0;
	}

	private Vector3 MirrorPosition(Vector3 originalPosition)
	{
		Vector3 relativePosition = originalPosition-bodyCenter.position;
		relativePosition.x = -relativePosition.x;

		return bodyCenter.position + relativePosition;
	}
}

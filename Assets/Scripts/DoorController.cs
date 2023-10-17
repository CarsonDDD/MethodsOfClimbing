using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

	private Vector3 closePosition;
	private Vector3 openPosition;

	[SerializeField]
	private float openDistance;

	[SerializeField]
	private float slideSpeed = 1.0f;

	private float currentLerpTime = 0;

	private bool isOpening = false;
	private bool isClosing = false;

	// Start is called before the first frame update
	void Start()
    {
		closePosition = GetComponent<Transform>().position;
		//openPosition = new Vector3(closePosition.x, closePosition.y, closePosition.z + openDistance);
		openPosition = transform.TransformPoint(new Vector3(0, 0, openDistance));

	}


	// Update is called once per frame
	void Update()
	{
		if(isOpening) {
			currentLerpTime += Time.deltaTime;
			float perc = currentLerpTime / slideSpeed;
			transform.position = Vector3.Lerp(closePosition, openPosition, perc);

			if(perc >= 1) {
				isOpening = false;
				currentLerpTime = 0;
			}
		}

		if(isClosing) {
			currentLerpTime += Time.deltaTime;
			float perc = currentLerpTime / slideSpeed;
			transform.position = Vector3.Lerp(openPosition, closePosition, perc);

			if(perc >= 1) {
				isClosing = false;
				currentLerpTime = 0;
			}
		}
	}

	public void Open()
	{
		if(!isOpening) {
			isOpening = true;
			isClosing = false;
			currentLerpTime = 0;
			Debug.Log("Door Opening");
		}
	}

	public void Close()
	{
		if(!isClosing) {
			isClosing = true;
			isOpening = false;
			currentLerpTime = 0;
			Debug.Log("Door Closing");
		}
	}
}

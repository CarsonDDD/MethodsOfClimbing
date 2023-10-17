using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    private bool isActive = false; //remove this later, figure out which active to get

    public Transform cameraTrans;
	public float distance = 2.0f;
	public float positionLerp = 0.125f;
    public float rotationLerp = 0.125f;

	// Start is called before the first frame update
	void Start()
    {
        menu.SetActive(isActive);

	}

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPerformedThisFrame()) {
            isActive = !isActive;
            menu.SetActive(isActive);
        }

        if(isActive) {
			Vector3 desiredPosition = cameraTrans.position + cameraTrans.forward * distance;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, positionLerp);
			transform.position = smoothedPosition;

			transform.rotation = Quaternion.Slerp(transform.rotation, cameraTrans.rotation, rotationLerp);
		}
    }
}

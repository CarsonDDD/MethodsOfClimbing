using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ClimbProvider : MonoBehaviour
{
    public CharacterController characterController;
    public InputActionProperty velocityRight;
    public InputActionProperty velocityLeft;

	public DynamicMoveProvider gravityProvider;

    private bool rightActive = false;
    private bool leftActive = false;

	// Start is called before the first frame update
	void Start()
    {
        XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
		XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;
	}

	private void OnDestroy()
	{
		XRDirectClimbInteractor.ClimbHandActivated -= HandActivated;
		XRDirectClimbInteractor.ClimbHandDeactivated -= HandDeactivated;
	}

	private void HandActivated(String controllerTag)
	{
		if(controllerTag == "LeftHand Controller") {
			leftActive = true;
			rightActive = false;
			Debug.Log("LeftHand activted");
		}
		else{
			rightActive = true;
			leftActive = false;
			Debug.Log("RightHand activted");
		}

		//disable gravity
		gravityProvider.useGravity = false;

	}

	private void HandDeactivated(String controllerTag)
	{
		if(rightActive && controllerTag == "RightHand Controller") {
			rightActive = false;
			Debug.Log("RightHand Deactivted");
			//enable gravity
			gravityProvider.useGravity = true;
		}
		else if(leftActive && controllerTag == "LeftHand Controller") {
			leftActive = false;
			Debug.Log("LeftHand Deactivted");
			gravityProvider.useGravity = true;
			//enable gravity
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if(rightActive || leftActive) {
			Climb();
		}        
    }

	private void Climb()
	{
		Vector3 velocity = leftActive ? velocityLeft.action.ReadValue<Vector3>() : velocityRight.action.ReadValue<Vector3>();
		//Debug.Log("Cilmb Velocity: " + velocity.ToString());

		characterController.Move(characterController.transform.rotation * -velocity * Time.fixedDeltaTime);

		//Debug.Log("Climbing!");
	}
}

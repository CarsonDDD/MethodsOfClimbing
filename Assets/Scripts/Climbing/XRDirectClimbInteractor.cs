using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// extending the directInteractor to have a custom event of interacting with an interactable with the 'Climable' tag
// This script is a replacement for the normal XRDirectInteractor.
// ClimbProvider, which gets attached to the XROrigin is where climbing takes place
public class XRDirectClimbInteractor : XRDirectInteractor
{
	public static Action<string> ClimbHandActivated;
	public static Action<string> ClimbHandDeactivated;

	private string controllerTag;// Used to differentiate between left and right controller, values SHOULD only be 'LeftHand Controller' or 'RightHand Controller'

	protected override void Start()
	{
		base.Start();
		controllerTag = gameObject.tag;
	}

	protected override void OnSelectEntered(SelectEnterEventArgs args)
	{
		base.OnSelectEntered(args);

		if(args.interactableObject.transform.gameObject.tag == "Climbable") {
			ClimbHandActivated?.Invoke(controllerTag);
		}
	}

	protected override void OnSelectExited(SelectExitEventArgs args)
	{
		base.OnSelectExited(args);

		ClimbHandDeactivated?.Invoke(controllerTag);
	}
}

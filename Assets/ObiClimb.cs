using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

[RequireComponent(typeof(ObiSolver))]
public class ObiClimb : MonoBehaviour
{
	public ObiSolver solver;

	public float releaseVelocityTimeOffset = -0.011f;
	public CharacterController characterController;
	private bool detectingCollisions = true;
	public DynamicMoveProvider gravityProvider;

	private XRBaseInteractor draggingHand = null;

	// Start is called before the first frame update
	void Start()
	{
		XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
		XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;

		if(detectingCollisions) {
			solver.OnCollision += Solver_OnCollision;
			detectingCollisions = false;
		}

	}

	void OnDestroy()
	{
		XRDirectClimbInteractor.ClimbHandActivated -= HandActivated;
		XRDirectClimbInteractor.ClimbHandDeactivated -= HandDeactivated;
	}

	void Update()
	{

	}

	void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
	{
		Debug.Log("Collision Detected!");
		Debug.Log("Total contacts: " + e.contacts.Count);

		var world = ObiColliderWorld.GetInstance();
		foreach(Oni.Contact contact in e.contacts) {
			Debug.Log("Contact distance: " + contact.distance);

			if(contact.distance < 0.01) {
				Debug.Log("Collision within threshold.");
				ObiColliderBase collider = world.colliderHandles[contact.bodyB].owner;
				Debug.Log("Collider tag: " + collider.gameObject.tag);

				if(collider != null && (collider.gameObject.tag == "LeftHand Controller" || collider.gameObject.tag == "RightHand Controller")) {
					Debug.Log($"Collision with {collider.gameObject.tag}");
					draggingHand = collider.gameObject.GetComponent<XRBaseInteractor>();
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(draggingHand && draggingHand.hasSelection) {
			Climb();
		}
	}

	private void HandActivated(String controllerTag)
	{
		Debug.Log($"Hand Activated: {controllerTag}");
		gravityProvider.useGravity = false;
	}

	private void HandDeactivated(String controllerTag)
	{
		Debug.Log($"Hand Deactivated: {controllerTag}");
		gravityProvider.useGravity = true;
	}

	private void Climb()
	{
		Vector3 velocity = draggingHand.attachTransform.position;
		characterController.Move(characterController.transform.rotation * -velocity * Time.fixedDeltaTime);
	}
}
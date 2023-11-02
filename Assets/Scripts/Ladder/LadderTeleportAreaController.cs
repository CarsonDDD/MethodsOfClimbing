using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadderTeleportAreaController : LadderTeleportController
{

	[SerializeField] protected AdvancedCollider teleportRegion;
	[SerializeField] protected AdvancedCollider previewRegion;

	private bool playerInTrigger = false; // overlap fix, maybe should be a map for multiple users??!?! naw

	private void Start()
	{
		// Hide meshes
		/*GameObject teleObj = teleportRegion.gameObject;
		GameObject prevObj = previewRegion.gameObject;

		MeshRenderer teleMesh = null;
		MeshRenderer prevMesh = null;
		if(teleObj.TryGetComponent<MeshRenderer>(out teleMesh)) {
			teleMesh.enabled = false;
		}

		if(prevObj.TryGetComponent<MeshRenderer>(out prevMesh)) {
			prevMesh.enabled = false;
		}*/

		// subscibe events
		teleportRegion.onTriggerEnter.AddListener(OnTeleportTriggerEnter);
		teleportRegion.onTriggerExit.AddListener(OnTeleportTriggerExit);

		previewRegion.onTriggerEnter.AddListener(OnPreviewTriggerEnter);
		previewRegion.onTriggerExit.AddListener(OnPreviewTriggerExit);
	}

	public void OnPreviewTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player")) {
			Debug.Log("Player in preview");
			EnablePreview();
		}
	}

	public void OnPreviewTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player")) {
			DisablePreview();
		}
	}

	public void OnTeleportTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player") && !playerInTrigger) {
			playerInTrigger = true;
			Debug.Log("Teleporting player!");
			//ActivateEventArgs args = new ActivateEventArgs(); TODO: If I ever make Teleport use args, I need to fix this

			Teleport(new ActivateEventArgs());
		}
	}

	public void OnTeleportTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player")) {
			// Fixes infinite loop bug if the destination is in the trigger region
			playerInTrigger = false;
		}
	}


	void EnablePreview()
	{

	}

	void DisablePreview()
	{

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadderTeleportController : MonoBehaviour
{
    
    [SerializeField] protected Transform highPoint;
    [SerializeField] protected Transform lowPoint;

    [SerializeField] protected bool forceRotation = true;

	// Hide zone meshes on start
	public void Start()
	{
        // Hide Meshes
        /*GameObject highObj = highPoint.gameObject;
		GameObject lowObj = highPoint.gameObject;

        MeshRenderer highMesh = null;
        MeshRenderer lowMesh = null;
        if(highObj.TryGetComponent<MeshRenderer>(out highMesh)) {
            highMesh.enabled = false;
        }

		if(lowObj.TryGetComponent<MeshRenderer>(out lowMesh)) {
            lowMesh.enabled = false;
        }*/
	}

	public virtual void Teleport(ActivateEventArgs args)
	{
        //IXRInteractor target = args.interactorObject;
        GameObject target = GameObject.FindWithTag("Player");// TODO: This is not generic

        //target.transform.position = highPoint.position;

        float padding = 0.5f;

		// edge cases are up to design.
		if(target.transform.position.y + padding < highPoint.position.y) {
            //go up
            target.transform.position = highPoint.position;

            if(forceRotation) {
                target.transform.rotation = highPoint.rotation;
            }
        }
        else {
            //go down
            target.transform.position = lowPoint.position;

            if(forceRotation ) {
                target.transform.rotation = lowPoint.rotation;
            }
		}
	}
}

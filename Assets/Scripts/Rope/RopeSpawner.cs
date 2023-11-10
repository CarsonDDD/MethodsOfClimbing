using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RopeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject jointPrefab;

    [SerializeField]
    [Range(1, 10)]
    private int length = 1;

    [SerializeField]
    float jointDistance = 0.25f;

    [SerializeField]
    private bool reset, spawn, hangInAir;

	private void OnValidate()
	{
		// The `#if UNITY_EDITOR` preprocessor directive is used to ensure that the code within it is only executed in the Unity editor and not in a built game.
        #if UNITY_EDITOR

		if(spawn) {
			Spawn();
			spawn = false;
		}
        #endif
	}

	// Update is called once per frame
	void Update()
    {
        if(reset) {
			//remove sub children
			while(transform.childCount > 0) {
				DestroyImmediate(transform.GetChild(0).gameObject);
			}
			reset = false;
            Spawn();
        }

        if(spawn) {
            Spawn();
            spawn = false;
        }
    }

    public void Spawn()
    {
        int jointsToSpawn = (int)(length/jointDistance);

        GameObject previousJoint = transform.gameObject; // First link in sausage is the spawner
		GameObject joint;

		if(hangInAir) {
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
        else {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}

		for(int i =0; i < jointsToSpawn; i++) {
			Vector3 spawnLocation = transform.position + transform.up * jointDistance * i;
			joint = Instantiate(jointPrefab, spawnLocation, Quaternion.identity, transform);
			joint.transform.rotation = transform.rotation;
            joint.transform.Rotate(Vector3.right, 180f);
			joint.name = "joint " + transform.childCount;

            // If this is the first joint in the sequence, we dont want it to be attached to anything (things are attached to it)
            /*if(i == 0 previousJoint == null) {
                //Destroy(joint.GetComponent<HingeJoint>());
                // Maybe not destroy the hinge?
                //The hinge isnt getting destroyed?
                // This definitely doesnt destroy the hinge

                // Maybe hinge to a parent rigid?
                previousJoint = transform.gameObject;// Cant do .this?
            }
            else {
			}*/
			joint.GetComponent<HingeJoint>().connectedBody = previousJoint.GetComponent<Rigidbody>();

            previousJoint = joint;
        }

    }
}

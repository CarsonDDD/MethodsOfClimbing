using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject jointPrefab;

    [SerializeField]
    [Range(1, 500)]
    private int length = 1;

    [SerializeField]
    float jointDistance = 0.25f;

    [SerializeField]
    private bool reset, spawn, hangInAir;
    
    

    // Update is called once per frame
    void Update()
    {
        if(reset) {
			//remove sub children
			while(transform.childCount > 0) {
				DestroyImmediate(transform.GetChild(0).gameObject);
			}
			reset = false;
        }

        if(spawn) {
            Spawn();
            spawn = false;
        }
    }

    public void Spawn()
    {
        int jointsToSpawn = (int)(length/jointDistance);

        GameObject previousJoint = null; // Temp used when linking sasuages

        for(int i =0; i < jointsToSpawn; i++) {
            GameObject joint;
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y + (jointDistance * i), transform.position.z);
            joint = Instantiate(jointPrefab, spawnLocation, Quaternion.identity, transform);
            joint.transform.eulerAngles = new Vector3(180, 0,0 );

            joint.name = "joint " + transform.childCount;

            // If this is the first joint in the sequence, we dont want it to be attached to anything (things are attached to it)
            // I can also check if prev joint is null
            if(previousJoint == null) {
                Destroy(joint.GetComponent<HingeJoint>());

                if(hangInAir) {
                    joint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else {
				// Connect the joint to the previous joint
				// Connecting based off string name is bad
				//joint.GetComponent<HingeJoint>().connectedBody = transform.Find("joint " + (transform.childCount - 1).ToString()).GetComponent<Rigidbody>();

				joint.GetComponent<HingeJoint>().connectedBody = previousJoint.GetComponent<Rigidbody>();
			}

            previousJoint = joint;
        }

    }
}

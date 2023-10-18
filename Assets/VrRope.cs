using Obi;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ObiRope))]
public class VrRope : MonoBehaviour
{

    [SerializeField]
    private GameObject grabablePrefab;

    private ObiRope obiRope;

    private List<GameObject> grabables = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        obiRope = GetComponent<ObiRope>();

        InitalizeHandles();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHandles();
    }

	public void InitalizeHandles()
	{
        // For each point, we add own own handle object
        // Since we can only get the vec3 pos, we must update frequently
		foreach(ObiWingedPoint point in obiRope.path.points.data) {
            Vector3 targetPosition = point.position;
            
            GameObject grabPoint = Instantiate(grabablePrefab, targetPosition, Quaternion.identity, transform);
            grabables.Add(grabPoint);

            //obiRope.path.OnControlPointAdded += 

            /*ObiParticleAttachment attachment = new ObiParticleAttachment();
            this.AddComponent<ObiParticleAttachment>(attachment);
            //attachment.attachmentType
            attachment.target = grabPoint.transform;
            attachment.particleGroup = grabPoint.GetComponent<ObiParticleGroup>();
            attachment.particleGroup.blueprint.groups*/
            // Other modifications?
        }
	}



    public void UpdateHandles()
    {
        List<ObiWingedPoint> points = obiRope.path.points.data;

		if(points.Count == grabables.Count) {

            for(int i =0; i< points.Count; i++) {

                grabables[i].transform.position = points[i].position;

                // Rotation is not updating, I cannot grab the rotation from the point.
                // I can see the rotation in the editor, however I dont know how I can get it in code.
                // However, if i use spheres rotation doesnt matter, however this may affect accuracy (sphere isnt rope shaped)
			}
        }
        else {
            // Error
            Debug.LogAssertion("Points does not fully participate with grabables");
        }
	}
}

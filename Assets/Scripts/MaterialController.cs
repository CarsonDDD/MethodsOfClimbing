using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField]
    private Material baseMaterial;

    void Start()
    {
        ResetColor();
    }

	public void ResetColor()
	{
		ChangeColor(baseMaterial);
	}

	public void ChangeColor(Material newMaterial)
    {
		List<Transform> allChildren = new List<Transform>();

		GetAllChildren(transform, allChildren);// deeper children dont get touched in a normal foreach

		foreach(Transform child in allChildren) {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();

            if(meshRenderer != null) {
                meshRenderer.material = newMaterial;
            }
        }
    }


	private void GetAllChildren(Transform parent, List<Transform> output)
	{
		foreach (Transform child in parent) {
            output.Add(child);
            GetAllChildren(child, output);
        }
	}



}

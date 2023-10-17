using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LadderGenerator : MonoBehaviour
{
	[SerializeField] public GameObject ladderSidePrefab;
	[SerializeField] public GameObject ladderHandlePrefab;

	[SerializeField] public float ladderHeight;
	[SerializeField] public float ladderWidth;
	[SerializeField] public float handleSpacing;

	[SerializeField] public float handleSize = 0.07f;

	[SerializeField] private bool generate = false;

	private GameObject offsetObject;

	private void OnValidate()
	{
		// The `#if UNITY_EDITOR` preprocessor directive is used to ensure that the code within it is only executed in the Unity editor and not in a built game.
		#if UNITY_EDITOR
			generate = false;
			GenerateLadder();
		#endif
	}

	public void GenerateLadder()
	{
		// if ladder already exists, remove it
		if(offsetObject != null) {
			DestroyImmediate(offsetObject);
		}

		// Create offset object
		offsetObject = new GameObject("Ladder Offset");
		offsetObject.transform.parent = transform;
		offsetObject.transform.localPosition = new Vector3(0f, ladderHeight, 0f);

		// set sides
		GameObject side1 = Instantiate(ladderSidePrefab, offsetObject.transform);
		GameObject side2 = Instantiate(ladderSidePrefab, offsetObject.transform);

		side1.transform.localPosition = new Vector3(-ladderWidth / 2f, 0f, 0f);
		side2.transform.localPosition = new Vector3(ladderWidth / 2f, 0f, 0f);

		side1.transform.localScale = new Vector3(side1.transform.localScale.x, ladderHeight, side1.transform.localScale.z);
		side2.transform.localScale = new Vector3(side2.transform.localScale.x, ladderHeight, side2.transform.localScale.z);

		// Set handles
		int handleCount = Mathf.FloorToInt(ladderHeight / handleSpacing) * 2;
		for(int i = 0; i < handleCount; i++) {
			GameObject handle = Instantiate(ladderHandlePrefab, offsetObject.transform);
			handle.transform.localPosition = new Vector3(0f, i * handleSpacing - ladderHeight / 2f, 0f);

			// Scale handle
			handle.transform.localScale = new Vector3(handleSize, ladderWidth/2, handleSize);
		}

		//transform.position += new Vector3(0f, ladderHeight / 2, 0f);
	}
}

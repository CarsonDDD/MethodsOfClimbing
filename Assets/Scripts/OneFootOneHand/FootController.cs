using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FootController : MonoBehaviour
{
	//functionality to either control its verticality, or drop it straight to the ground. probably vertical control.

	// rotation reset

	// mirror mode?

	// potential 'belt'[

	[Range(0.001f, 0.5f)]
	[SerializeField] private float verticalMoveAmount;

	public GameObject foot;
	public InputActionReference lowerFootAction;
	public InputActionReference raiseFootAction;

	private bool isLowering = false;
	private bool isRaising = false;
	
	void OnEnable()
	{
		lowerFootAction.action.started += LowerFoot;

		raiseFootAction.action.started += RaiseFoot;

		lowerFootAction.action.started += e => isLowering = true;
		lowerFootAction.action.canceled += e => isLowering = false;

		raiseFootAction.action.started += e => isRaising = true;
		raiseFootAction.action.canceled += e => isRaising = false;
	}

	void OnDisable()
	{
		lowerFootAction.action.started -= LowerFoot;
		raiseFootAction.action.started -= RaiseFoot;

		lowerFootAction.action.started -= e => isLowering = true;
		lowerFootAction.action.canceled -= e => isLowering = false;

		raiseFootAction.action.started -= e => isRaising = true;
		raiseFootAction.action.canceled -= e => isRaising = false;
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		// Not working
		if(isRaising && isLowering) {
			ResetFootRotation();
		}
	}

	private void RaiseFoot(InputAction.CallbackContext context)
	{
		Debug.Log("Foot Raised!");
		foot.transform.position += verticalMoveAmount * foot.transform.up;
	}

	private void LowerFoot(InputAction.CallbackContext context)
	{
		Debug.Log("Foot lowered!");
		foot.transform.position -= verticalMoveAmount * foot.transform.up;
	}

	private void ResetFootRotation()
	{
		Debug.Log("Foot rotation reset!");
		foot.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);

		// reset parent rotation to ignore magic num
		//transform.rotation = Quaternion.identity;
	}
}
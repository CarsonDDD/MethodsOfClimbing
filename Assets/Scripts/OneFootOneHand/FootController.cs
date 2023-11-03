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

	// potential 'belt'

	public GameObject foot;
	public InputActionReference lowerFootAction;
	public InputActionReference raiseFootAction;


	void OnEnable()
	{
		lowerFootAction.action.started += LowerFoot;
		raiseFootAction.action.started += RaiseFoot;
	}

	void OnDisable()
	{
		lowerFootAction.action.started -= LowerFoot;
		raiseFootAction.action.started -= RaiseFoot;
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void RaiseFoot(InputAction.CallbackContext context)
	{
		Debug.Log("Foot Raised!");
		foot.transform.position += 1f * foot.transform.up;
	}

	private void LowerFoot(InputAction.CallbackContext context)
	{
		Debug.Log("Foot lowered!");
		foot.transform.position -= 1f * foot.transform.up;
	}


}

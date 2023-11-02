using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lightsaber : MonoBehaviour
{
    public XRGrabInteractable lightsaber;
    public GameObject blade;

    private Vector3 bladeSize;
    public float growthSpeed;
	public bool isActive = false;

	[Range(0, 1)]
	public float hapticIntensity;
	public float hapticDuration;


	void Start()
    {
        // determined by object start
        bladeSize = blade.transform.localScale;

        if(!isActive) blade.transform.localScale = new Vector3(0, 0, 0);

		lightsaber.activated.AddListener(ToggleBlade);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) {
			//grow
			GrowBlade(bladeSize, growthSpeed);
		}
        else {
			//shrink
			GrowBlade(Vector3.zero, -growthSpeed);
		}
	}

    public void ToggleBlade(ActivateEventArgs args)
    {
        isActive = !isActive;
        Debug.Log("Shwoop!");

		if(args.interactorObject is XRBaseControllerInteractor controller) {
			controller.SendHapticImpulse(hapticIntensity, hapticDuration);
		}
	}

	private void GrowBlade(Vector3 targetSize, float speed)
	{
		Vector3 currentSize = blade.transform.localScale;

		float newX = Mathf.Clamp(currentSize.x + speed, 0, targetSize.x);
		float newY = Mathf.Clamp(currentSize.y + speed, 0, targetSize.y);
		float newZ = Mathf.Clamp(currentSize.z + speed, 0, targetSize.z);

		blade.transform.localScale = new Vector3(newX, newY, newZ);
	}
}

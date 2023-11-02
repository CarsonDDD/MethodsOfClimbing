using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RopeJoint : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float hapticIntensity;
    
    [SerializeField]
    private float hapticDuration;
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(TriggerHaptic);
    }

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if(eventArgs.interactableObject is XRBaseControllerInteractor controller) {
            TriggerHaptic(controller.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if(hapticIntensity > 0f) {
            controller.SendHapticImpulse(hapticIntensity,hapticDuration);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRFootManager : MonoBehaviour
{
	// mirror mode?

	public FootController rightFoot;
    public GameObject rightHand;

    private bool isFootEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        DisableFoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFoot()
    {
        isFootEnabled = !isFootEnabled;
        if(isFootEnabled) {
            EnableFoot();
        }
        else {
            DisableFoot();
        }
    }

    public void EnableFoot()
    {
        rightFoot.transform.gameObject.SetActive(true);
        rightHand.SetActive(false);
    }

	public void DisableFoot()
	{
		rightFoot.transform.gameObject.SetActive(false);
		rightHand.SetActive(true);
	}
}

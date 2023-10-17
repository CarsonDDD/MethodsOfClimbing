using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : ObjectTracker
{
    [SerializeField]
    private GameObject headObject;
    [SerializeField]
    private GameObject leftHandObject;
    [SerializeField]
    private GameObject rightHandObject;

    private ObjectTracker headTracker;
    private ObjectTracker leftHandTracker;
    private ObjectTracker rightHandTracker;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // 'this' is the body of the player so reset world correctly

        // probably use array and loop for sub items if there is more?
        headTracker = CreateTracker(headObject);
        leftHandTracker = CreateTracker(leftHandObject);
        rightHandTracker = CreateTracker(rightHandObject);

        configureChildTracker(headTracker);
        configureChildTracker(leftHandTracker);
        configureChildTracker(rightHandTracker);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void configureChildTracker(ObjectTracker tracker)
    {
        tracker.spamConsole = this.spamConsole;
        tracker.outputToFile = this.outputToFile;
        tracker.framesPerTrack = this.framesPerTrack;
    }

    public override string GetFormattedText()
    {
        return "Head:\n" + headTracker.GetFormattedText() +
            "\n\nBody:\n" + base.GetFormattedText() +
            "\n\nLeft Hand:\n" + leftHandTracker.GetFormattedText() +
            "\n\nRight Hand:\n" + rightHandTracker.GetFormattedText();
    }


}

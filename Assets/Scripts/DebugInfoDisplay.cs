using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfoDisplay : MonoBehaviour
{
    public Text output;
    
    public ObjectTracker obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();        
    }

    public void UpdateDisplay(){
        /*if (output != null)
        {
            if(player != null)
            {
                if(player.HeadTracker != null)
                {
                    output.text =
                    "Position: " + player.GetPosition() + "\n" +
                    "Rotation: " + player.GetRotation() + "\n" +
                    "Head Position: " + player.HeadTracker.GetPosition() + "\n" +
                    "Head Rotation: " + player.HeadTracker.GetRotation();
                }
                else
                {
                    Debug.LogError("Head is null!");
                }
            }
            else
            {
                Debug.LogError("Player is null!");
            }

        }
        else
        {
            Debug.LogError("Text is null!");
        }*/

        output.text = obj.GetFormattedText();
    }
}

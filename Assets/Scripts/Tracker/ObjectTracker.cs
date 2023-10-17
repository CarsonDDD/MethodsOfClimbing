using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    private static List<ObjectTracker> objectTrackers = new List<ObjectTracker>();
    private static string outputLocation = "output/";
    private class TrackerData
    {
		public Vector3 position;
		public Vector3 rotation;
		public Vector3 scale;

        // Realistically, this should take a GameObject since we probably want more then just transform
		public TrackerData(Transform transform)
		{
			position = transform.position;
			rotation = transform.eulerAngles;
			scale = transform.localScale;
		}

        public String GetJson()
        {
            StringBuilder sb = new StringBuilder();

			sb.Append("{");
			sb.AppendFormat("\"position\":[{0},{1},{2}],", position.x, position.y, position.z);
			sb.AppendFormat("\"rotation\":[{0},{1},{2}],", rotation.x, rotation.y, rotation.z);
			sb.AppendFormat("\"scale\":[{0},{1},{2}]", scale.x, scale.y, scale.z);
			sb.Append("}");

            return sb.ToString();
		}
	} 


	private List<TrackerData> objectHistory = new List<TrackerData>();

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;

	public bool spamConsole = true;
    public bool outputToFile = true;
    public bool pause = false;// Controls if the any tracking or logging should take place.

    public int framesPerTrack = 10;
    private int frameCounter;

    // Json String doesnt get generated more then it needs to (only once per history)
    private String _json;
	private String JSON {
        get {
            if(_json == null || _json.Length == 0) _json = GenerateJsonString();

            return _json;
        }
    }

    //private Collider? collider = null;

    protected void Start()
    {
        objectTrackers.Add(this);

        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;

        frameCounter = framesPerTrack; // this makes sure we always track the first frame

        //TryGetComponent<Collider>(out collider);
    }

    void Update()
    {
        if(pause) return;

        frameCounter++;
        if (frameCounter >= framesPerTrack)
        {
			if(spamConsole) {
				Debug.Log(GetFormattedText());
			}

			objectHistory.Add(new TrackerData(transform));
            _json = null;// History has changed so json is no longer accurate

            frameCounter = 0;
		}
    }
	private void OnApplicationQuit()
	{
		Debug.Log(JSON);// remove later

        if(outputToFile) {
		    ExportJson();
        }
	}

	public static ObjectTracker CreateTracker(GameObject gameObject)
    {
        ObjectTracker tracker = gameObject.GetComponent<ObjectTracker>();
        if (tracker == null)
        {
            tracker = gameObject.AddComponent<ObjectTracker>();
        }
        return tracker;
    }

	/*
    "name": "name",
    "history":[{item},{item},{item}]
    */
	private string GenerateJsonString()
    {
        StringBuilder sb = new StringBuilder();

		sb.Append("{");
		sb.AppendFormat("\"name\":\"{0}\",", this.name);

        // array
		sb.Append("\"history\":[");
		for(int i = 0; i < objectHistory.Count; i++) {
			TrackerData data = objectHistory[i];

            // item
            sb.Append(data.GetJson());

            if(i != objectHistory.Count - 1) {
				sb.Append(",");
			}
		}
		sb.Append("]}");

		return sb.ToString();
    }

    public void ExportJson()
    {

		if(!Directory.Exists(outputLocation)) {
			Directory.CreateDirectory(outputLocation);
		}

		String filename = outputLocation + this.name + "_tracker.json";
        File.WriteAllText(filename, JSON);
    }

	public void ResetPosition()
	{
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.localScale = startScale;

		Debug.Log("Resetting " + this.name + "'s position!");
	}

	public virtual String GetFormattedText()
	{
		return "Position: " + transform.position + "\n" + "Rotation: " + transform.eulerAngles + "\n" + "Scale: " + transform.localScale;
	}

	#region Getters
	// -- Start Getters-- //
	public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public Quaternion GetStartRotation()
    {
        return startRotation;
    }

    public Vector3 GetStartScale()
    {
        return startScale;
    }
    #endregion
}

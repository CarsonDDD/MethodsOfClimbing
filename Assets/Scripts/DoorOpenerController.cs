using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenerController : MonoBehaviour
{

    public DoorController[] doors;

    public void OpenDoors()
    {
        foreach(DoorController door in doors) {
            door.Open();
        }
    }

	public void CloseDoors()
	{
		foreach(DoorController door in doors) {
			door.Close();
		}
	}

}

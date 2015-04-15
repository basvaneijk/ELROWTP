using UnityEngine;
using System.Collections;

public class CompassOrientation : MonoBehaviour
{
	public bool printDebug = false;

	float angleDiff;
	
	void Start ()
	{
		Input.compass.enabled = true;
		Input.location.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Quaternion compRot = Quaternion.Euler (0, Input.compass.trueHeading, 0);
		angleDiff = Mathf.Abs (Quaternion.Angle (Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0), compRot));

		transform.rotation = Quaternion.RotateTowards (transform.rotation, compRot, angleDiff / 25);

	}

	void OnGUI ()
	{
		if (printDebug) {
			GUI.Label (new Rect (10, 10, 100, 100), "Compas: " + Input.compass.trueHeading);
			GUI.Label (new Rect (10, 150, 100, 100), "angleDiff: " + angleDiff);
		}
	}
}

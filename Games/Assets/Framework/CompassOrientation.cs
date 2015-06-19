using UnityEngine;
using System.Collections;

public class CompassOrientation : MonoBehaviour
{
	public bool printDebug = false;
	public int rotationOffset = 0;
	float speed;
	Quaternion targetRotation;
	GUIStyle debugTextStyle;
	float direction;

    Quaternion northRotation;

	void Start ()
	{
		Input.compass.enabled = true;
		Input.gyro.enabled = true;

		debugTextStyle = new GUIStyle ();
		debugTextStyle.fontSize = 32;

        northRotation = Quaternion.Euler(new Vector3(0.0f, Input.compass.trueHeading + rotationOffset, 0.0f));
	}
	
	// Update is called once per frame
	void Update ()
	{

        targetRotation = ConvertRotation(Input.gyro.attitude);//Quaternion.Euler (new Vector3 (0.0f, Input.compass.trueHeading + rotationOffset, 0.0f));

        //speed = Mathf.Abs (Input.gyro.rotationRate.y) * 100.0f;

        //direction = Quaternion.Dot (targetRotation, transform.localRotation);
        //if (Mathf.Abs (direction) < 0.99f) {
        //    //transform.localRotation = targetRotation;
        //    transform.localRotation = Quaternion.RotateTowards (transform.rotation, targetRotation, 10.0f);
        //} else if (speed > 10.0f) {
        //    //transform.localRotation = Quaternion.RotateTowards (transform.rotation, targetRotation, speed);
        //    transform.localRotation = transform.localRotation * Quaternion.AngleAxis (-Input.gyro.rotationRate.y, Vector3.up);
        //}

        transform.localRotation = northRotation * targetRotation;
	}

	private static Quaternion ConvertRotation (Quaternion q)
	{
		return Quaternion.Euler (-270, 0, 0) * new Quaternion (q.x, q.y, -q.z, -q.w);
	}

	void OnGUI ()
	{
		if (printDebug) {
			GUI.Label (new Rect (10, 10, 200, 100), "Orientation: " + transform.localRotation.eulerAngles, debugTextStyle);
			GUI.Label (new Rect (10, 50, 200, 100), "TargetOrientation: " + targetRotation.eulerAngles, debugTextStyle);
			GUI.Label (new Rect (10, 100, 200, 100), "Compas: " + Input.compass.trueHeading, debugTextStyle);
			GUI.Label (new Rect (10, 150, 200, 100), "Gyro accel: " + Input.gyro.rotationRate, debugTextStyle);
			GUI.Label (new Rect (10, 200, 200, 100), "direction: " + direction, debugTextStyle);
		}
	}
}

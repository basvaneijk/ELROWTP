/*
    Framework for ELRO Wants To Play
    Copyright (C) 2015 Simon Voordouw
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using UnityEngine;
using System.Collections;

/**
 * Rotates the entity based on the gyroscope's attitude
 */
public class GyroOrientation : MonoBehaviour
{
	public bool printDebug = false;
	public bool highpassFilter = false;
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

		targetRotation = Quaternion.identity;

		northRotation = Quaternion.Euler (new Vector3 (0.0f, Input.compass.trueHeading + rotationOffset, 0.0f));
	}

	void Update ()
	{
		if (highpassFilter) {
			Vector3 gyroRot = Input.gyro.rotationRate;
			if (Mathf.Abs (gyroRot.x) < 0.08) {
				gyroRot.x = 0.0f;
			}

			if (Mathf.Abs (gyroRot.y) < 0.08f) {
				gyroRot.y = 0.0f;
			}

			if (Mathf.Abs (gyroRot.z) < 0.08f) {
				gyroRot.z = 0.0f;
			}

			targetRotation *= Quaternion.Euler (gyroRot);
		} else {
			targetRotation = Input.gyro.attitude;
		}

		transform.localRotation = northRotation * ConvertRotation (targetRotation);
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

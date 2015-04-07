using UnityEngine;
using System.Collections;
using System;

namespace Framework
{

	public abstract class LocationUpdateArgs : EventArgs
	{
		abstract public Vector3 Location ();
		abstract public float Accuracy ();
	}

	public delegate void LocationUpdateHandler (object source,LocationUpdateArgs e);

	interface LocationProvider
	{
		event LocationUpdateHandler OnLocationUpdate;
	}
}
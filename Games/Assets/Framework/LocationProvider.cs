using UnityEngine;
using System.Collections;
using System;

namespace Framework
{

	public class LocationUpdateArgs : EventArgs
	{
		int objectId;
		Vector3 location;
		float accuracy;

		public int ObjectId {
			get {
				return objectId;
			}
		}

		public Vector3 Location {
			get {
				return location;
			}
		}
		public float Accuracy {
			get {
				return accuracy;
			}
		}

		public LocationUpdateArgs (int objectId, Vector3 location, float accuracy)
		{
			this.objectId = objectId;
			this.location = location;
			this.accuracy = accuracy;
		}

		public override string ToString ()
		{
			return string.Format ("[LocationUpdateArgs: ObjectId={0}, Location={1}, Accuracy={2}]", objectId, location, accuracy);
		}
	}

	public delegate void LocationUpdateHandler (object source,LocationUpdateArgs e);

	interface LocationProvider
	{
		event LocationUpdateHandler OnLocationUpdate;
	}
}
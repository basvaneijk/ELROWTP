using UnityEngine;
using System.Collections;
using System;

namespace Framework
{
	/**
	 * Event arguments for location updates
	 */
	public class LocationUpdateArgs : EventArgs
	{
		int objectId;
		Vector3 location;
		float accuracy;

		/**
		 * Id of the updated position's object
		 */
		public int ObjectId {
			get {
				return objectId;
			}
		}

		/**
		 * New location
		 */
		public Vector3 Location {
			get {
				return location;
			}
		}

		public LocationUpdateArgs (int objectId, Vector3 location)
		{
			this.objectId = objectId;
			this.location = location;
		}

		public override string ToString ()
		{
			return string.Format ("[LocationUpdateArgs: ObjectId={0}, Location={1}, Accuracy={2}]", objectId, location);
		}
	}

	public delegate void LocationUpdateHandler (object source,LocationUpdateArgs e);

	interface LocationProvider
	{
		event LocationUpdateHandler OnLocationUpdate;
	}
}
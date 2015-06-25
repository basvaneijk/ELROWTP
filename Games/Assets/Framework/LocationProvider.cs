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
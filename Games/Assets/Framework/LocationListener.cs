using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Framework
{
	/**
	 * Registers for location updates from a LocationProvider on the same entity. 
	 * Translates the entity based on received updates with matching color
	 */
	public class LocationListener : MonoBehaviour
	{
		public PlayerColor color;
		LocationProvider locationProvider;
		Vector3 targetLocation;

		void Start ()
		{
			locationProvider = GetComponent<LocationProvider> ();

			locationProvider.OnLocationUpdate += (object source, LocationUpdateArgs e) => {
				if ((PlayerColor)e.ObjectId == color) {
					targetLocation = e.Location;
				}
			};
			targetLocation = transform.position;
		}

		void Update ()
		{
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, 0.08f);
		}
	}
}
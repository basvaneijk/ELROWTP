using UnityEngine;
using System.Collections;

namespace Framework
{
	public class LocationListener : MonoBehaviour
	{
		public PlayerColor color;
		LocationProvider locationProvider;

		// Use this for initialization
		void Start ()
		{
			locationProvider = GetComponent<LocationProvider> ();

			locationProvider.OnLocationUpdate += (object source, LocationUpdateArgs e) => {
				if ((PlayerColor)e.ObjectId == color) {
                    transform.position = Vector3.MoveTowards(transform.position, e.Location, 0.4f);
				}
			};
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Framework
{
	public class LocationListener : MonoBehaviour
	{
		LocationProvider locationProvider;

		// Use this for initialization
		void Start ()
		{
			locationProvider = GetComponent<LocationProvider> ();

			locationProvider.OnLocationUpdate += (object source, LocationUpdateArgs e) => {
				Debug.Log (e);
				transform.position = e.Location;
			};
		}
	}
}
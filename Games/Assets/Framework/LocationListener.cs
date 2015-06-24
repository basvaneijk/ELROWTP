using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Framework
{
	public class LocationListener : MonoBehaviour
	{
		public PlayerColor color;
		LocationProvider locationProvider;
		Vector3 targetLocation;

		// Use this for initialization
		void Start ()
		{
			locationProvider = GetComponent<LocationProvider> ();

			locationProvider.OnLocationUpdate += (object source, LocationUpdateArgs e) => {
				if ((PlayerColor)e.ObjectId == color) {
					targetLocation = e.Location;
					//GameObject.FindGameObjectWithTag ("Cords").GetComponent<Text> ().text = transform.position.ToString ();
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
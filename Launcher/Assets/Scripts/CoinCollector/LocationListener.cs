using UnityEngine;
using System.Collections;

namespace Framework
{
	public class LocationListener : MonoBehaviour
	{
		public PlayerColor color;
		LocationProvider locationProvider;
        Vector3 targetlocation;

		// Use this for initialization
		void Start ()
		{
			locationProvider = GetComponent<LocationProvider> ();

			locationProvider.OnLocationUpdate += (object source, LocationUpdateArgs e) => {
				if ((PlayerColor)e.ObjectId == color) {
                    targetlocation = e.Location;
				}
			};
		}
        void FixedUpdate()
        {
            if (Mathf.Abs((transform.position - targetlocation).magnitude) <= 0.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetlocation, 0.2f);
            }
            else{
                transform.position = targetlocation;
            }
            
        }
	}
}
using UnityEngine;
using System.Collections;

namespace Framework
{
	
	public class MockLocationProvider : MonoBehaviour, LocationProvider
	{
		public event LocationUpdateHandler OnLocationUpdate;
		
		void Start ()
		{
			StartCoroutine (SendFakeLocation ());
		}
		
		IEnumerator SendFakeLocation ()
		{
			while (true) {
				yield return new WaitForSeconds (1);
				
				var loc = new LocationUpdateArgs (1, Random.insideUnitSphere * 10);
				
				if (OnLocationUpdate != null) {
					OnLocationUpdate (this, loc);
				}
			}
		}
	}
}
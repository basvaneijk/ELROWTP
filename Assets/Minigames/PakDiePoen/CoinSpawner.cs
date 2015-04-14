using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class CoinSpawner : MonoBehaviour
{
	public GameObject coinPrefab;
	public int numCoins;
	List<GameObject> coins;

	void Start ()
	{
		coins = new List<GameObject> ();

		var mt = TrackerManager.Instance.InitTracker<MarkerTracker> ();
		//MarkerTracker mt = TrackerManager.Instance.GetTracker<MarkerTracker> ();

		for (int c = 0; c < numCoins; c++) {
			var marker = mt.CreateMarker (c, "coinMarker" + c, 60);
			marker.gameObject.AddComponent<DefaultTrackableEventHandler> ();
			coins.Add (marker.gameObject);
			marker.transform.parent = this.transform;
			
			var newCoin = Instantiate (coinPrefab);
			newCoin.transform.parent = marker.transform;
		}

		GameObject.Find ("ARCamera").SetActive (true);
	}
}

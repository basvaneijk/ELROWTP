using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Reticle") {
			gameObject.SetActive (false);
		}
	}
}

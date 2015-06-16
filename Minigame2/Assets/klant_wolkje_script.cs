using UnityEngine;
using System.Collections;

public class klant_wolkje_script : MonoBehaviour {
	bool enabled = false;
	public GameObject wolk;
	// Use this for initialization
	void Start () {
		wolk=GameObject.Find("klant_wolkje");
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			wolk.transform.Rotate (Vector3.up, 10 * Time.deltaTime);
		} else {
			wolk.transform.rotation = Quaternion.AngleAxis (180, Vector3.up);
			//wolk.SetActive (false);
		}
	}
}

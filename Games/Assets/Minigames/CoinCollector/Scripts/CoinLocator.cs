using UnityEngine;
using System.Collections;

public class CoinLocator : MonoBehaviour {

	private GameObject target;
	private GameObject locator;
	// Use this for initialization
	void Start () {
		locator = GameObject.FindGameObjectWithTag ("CoinLocatorArrow");
		target = FindClosestCoin ();
	}
	
	public GameObject FindClosestCoin() {
		GameObject[] gos;
		GameObject closest = null;
		GameObject start = GameObject.FindGameObjectWithTag ("Arrow");
		//print (start);
		if(start==null) {
			gos = GameObject.FindGameObjectsWithTag ("Coin");

			float distance = Mathf.Infinity;
			Vector3 position = Camera.main.transform.position;
			foreach (GameObject go in gos) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		} else {
			closest = start;
		}
		return closest;
	}
	// Update is called once per frame
	void Update () {
		if (target==null){
			target = FindClosestCoin ();
		} else {
			//Calculate the angle from the camera to the target
			Vector3 targetDir = target.transform.position - Camera.main.transform.position;
			Vector3 forward = Camera.main.transform.forward;
			float angle = Vector3.Angle(targetDir, forward)+90;

			float angledirection= AngleDir(forward,targetDir,Camera.main.transform.up);
			//If the angle exceeds 90deg inverse the rotation to point correctly
			if(angledirection==-1){
				locator.transform.localRotation = Quaternion.Euler(0,0,angle);
			} else {
				locator.transform.localRotation = Quaternion.Euler(0,0,180-angle);
			}
			//print ("angle: "+angle+" target loc: "+Camera.main.transform.forward);
		}
	}
	public float AngleDir(Vector3 fwd,Vector3 targetDir, Vector3 up) {
		Vector3 perp  = Vector3.Cross(fwd, targetDir);
		float dir  = Vector3.Dot(perp, up);
		
		if (dir > 0.0f) {
			return 1.0f;
		} else if (dir < 0.0f) {
			return -1.0f;
		} else {
			return 0.0f;
		}
	}
}

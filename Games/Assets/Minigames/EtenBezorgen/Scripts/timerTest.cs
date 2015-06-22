using UnityEngine;
using System.Collections;

public class timerTest : MonoBehaviour {
	float counter;
	bool bTimer;
	
	void Start () {
		startTimer(3);
	}

	void Update () {
		if (bTimer) {
			counter -= Time.deltaTime;
			Debug.Log ((int) counter);
			if (counter <= 0) {
				bTimer = false;
				Debug.Log ("DOING! TIME'S UP!");
			}
		}
	}

	void startTimer(int time) {
		bTimer = true;
		counter = time;
	}
}
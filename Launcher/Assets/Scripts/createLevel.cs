using UnityEngine;
using System.Collections;

public class createLevel : MonoBehaviour {

	//scale 0.1 is 1m
	//dus een scale van 1.0 is 10m
	float width;
	float length;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetFloat("width") > 0 && PlayerPrefs.GetFloat("length") > 0) {
			transform.localScale = new Vector3(getScale(PlayerPrefs.GetFloat("width")), 0.1F, getScale(PlayerPrefs.GetFloat("length")));
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("1")) {

			width = 10;
			length = 10;

			PlayerPrefs.SetFloat("width", width);
			PlayerPrefs.SetFloat("length", length);

			transform.localScale = new Vector3(getScale(width), 0.1F, getScale(length));
		}

		if (Input.GetKeyDown("2")) {

			width = 100;
			length = 100;

			PlayerPrefs.SetFloat("width", width);
			PlayerPrefs.SetFloat("length", length);

			transform.localScale = new Vector3(getScale(width), 0.1F, getScale(length));
		}
	
	}

	float getScale(float data) {
		return data / 10;
	}

}

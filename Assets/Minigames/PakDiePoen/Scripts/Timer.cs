using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public float minutes = 0f;
	public float seconds = 0f;
	public float milliseconds = 0f;


	// Use this for initialization
	void Start () {
		//seconds = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		seconds += Time.deltaTime;
		milliseconds += Time.deltaTime;
	}
}

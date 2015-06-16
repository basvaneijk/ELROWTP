using UnityEngine;
using System.Collections;

public class gameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void returnToMenu() {
		Application.LoadLevel(PlayerPrefs.GetString("menu"));
	}

}
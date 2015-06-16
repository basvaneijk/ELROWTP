using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class miniGame1Menu : MonoBehaviour {
	
	public GameObject helpFrame;
	public Button CoinsLevel1, CoinsLevel2, CoinsLevel3;
	public Sprite CoinsLevel1Normal, CoinsLevel1Lock, CoinsLevel2Normal, CoinsLevel2Lock, CoinsLevel3Normal, CoinsLevel3Lock;
	
	void Start () {

		PlayerPrefs.SetInt("levelCleared", 0);

		int levelCleared = PlayerPrefs.GetInt("levelCleared");
		if (levelCleared >= 1) {
			CoinsLevel2.interactable = true;
		} else {
			CoinsLevel2.interactable = false;
			CoinsLevel2.image.sprite = CoinsLevel2Lock;
		}
		if (levelCleared >= 2) {
			CoinsLevel3.interactable = true;
		} else {
			CoinsLevel3.interactable = false;
			CoinsLevel3.image.sprite = CoinsLevel3Lock;
		}

	}

	void Update () {
		 
	}

	public void toggleHelp() {
		if (helpFrame.activeSelf == true) {
			helpFrame.SetActive(false);
		} else {
			helpFrame.SetActive(true);
		}
	}

	public void startCoinsLevel1() {
		PlayerPrefs.SetString("menu", "miniGame1Menu");
		Application.LoadLevel("miniGame1");
	}
	public void startCoinsLevel2() {
		Application.LoadLevel("miniGame1");
	}
	public void startCoinsLevel3() {
		Application.LoadLevel("miniGame1");
	}

	public void returnToMain() {
		Application.LoadLevel("main");
	}

}
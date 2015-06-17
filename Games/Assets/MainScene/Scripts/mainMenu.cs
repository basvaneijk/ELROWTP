using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenu : MonoBehaviour {

	public InputField inputLength, inputWidth;
	public GameObject helpFrame, settingsFrame;
	public Button ButtonBlue, ButtonRed, ButtonGreen, MiniGame1, MiniGame2, MiniGame3;
	public Sprite ButtonBlueNormal, ButtonBlueActive, ButtonRedNormal, ButtonRedActive, ButtonGreenNormal, ButtonGreenActive, MiniGame1Normal, MiniGame1Active, MiniGame2Normal, MiniGame2Active, MiniGame3Normal, MiniGame3Active;
	
	void Start () {
		string levelLengthString = PlayerPrefs.GetFloat("length").ToString();
		string levelWidthString = PlayerPrefs.GetFloat("width").ToString();
		
		if (levelLengthString != "" && levelWidthString != "") {
			inputLength.text = levelLengthString;
			inputWidth.text = levelWidthString;
		}

		switch (PlayerPrefs.GetString("color")) {
			case "blue":
				ButtonBlue.image.sprite = ButtonBlueActive;
				break;
			case "red":
				ButtonRed.image.sprite = ButtonRedActive;
				break;
			case "green":
				ButtonGreen.image.sprite = ButtonGreenActive;
				break;
			default:
				break;
		}

		switch (PlayerPrefs.GetString("game")) {
			case "miniGame1Menu":
				MiniGame1.image.sprite = MiniGame1Active;
				break;
			case "miniGame1":
				MiniGame2.image.sprite = MiniGame2Active;
				break;
			case "miniGame3":
				MiniGame3.image.sprite = MiniGame3Active;
				break;
			default:
				break;
		}

	}

	void Update () {
		 
	}

	public void toggleHelp() {
		if (helpFrame.activeSelf == true) {
			helpFrame.SetActive(false);
		} else {
			settingsFrame.SetActive(false);
			helpFrame.SetActive(true);
		}
	}
	public void toggleSettings() {
		if (settingsFrame.activeSelf == true) {
			inputLength.text = PlayerPrefs.GetFloat("length").ToString();
			inputWidth.text = PlayerPrefs.GetFloat("width").ToString();
			settingsFrame.SetActive(false);
		} else {
			helpFrame.SetActive(false);
			settingsFrame.SetActive(true);
		}
	}

	public void saveSettings() {
		PlayerPrefs.SetFloat("width", float.Parse(inputWidth.text));
		PlayerPrefs.SetFloat("length", float.Parse(inputLength.text));
		toggleSettings();
	}

	public void startGame() {
		//if (PlayerPrefs.GetString ("color") && PlayerPrefs.GetString ("game") && PlayerPrefs.GetFloat ("length") && PlayerPrefs.GetFloat ("width")) {
		PlayerPrefs.SetString("menu", "main");	
		Application.LoadLevel(PlayerPrefs.GetString("game"));
		//} else {
		//	Debug.Log ("Niet alle velden zijn correct ingevuld!");
		//}
	}
	public void endGame() {
		Application.Quit();
	}

	public void setColorBlue() {
		PlayerPrefs.SetString("color", "blue");
		ButtonBlue.image.sprite = ButtonBlueActive;
		ButtonRed.image.sprite = ButtonRedNormal;
		ButtonGreen.image.sprite = ButtonGreenNormal;
	}
	public void setColorRed() {
		PlayerPrefs.SetString("color", "red");
		ButtonBlue.image.sprite = ButtonBlueNormal;
		ButtonRed.image.sprite = ButtonRedActive;
		ButtonGreen.image.sprite = ButtonGreenNormal;
	}
	public void setColorGreen() {
		PlayerPrefs.SetString("color", "green");
		ButtonBlue.image.sprite = ButtonBlueNormal;
		ButtonRed.image.sprite = ButtonRedNormal;
		ButtonGreen.image.sprite = ButtonGreenActive;
	}

	public void setMiniGame1() {
		PlayerPrefs.SetString("game", "miniGame1Menu");
		MiniGame1.image.sprite = MiniGame1Active;
		MiniGame2.image.sprite = MiniGame2Normal;
		MiniGame3.image.sprite = MiniGame3Normal;
	}
	public void setMiniGame2() {
		PlayerPrefs.SetString("game", "miniGame1");
		MiniGame1.image.sprite = MiniGame1Normal;
		MiniGame2.image.sprite = MiniGame2Active;
		MiniGame3.image.sprite = MiniGame3Normal;
	}
	public void setMiniGame3() {
		PlayerPrefs.SetString("game", "miniGame1");
		MiniGame1.image.sprite = MiniGame1Normal;
		MiniGame2.image.sprite = MiniGame2Normal;
		MiniGame3.image.sprite = MiniGame3Active;
	}

}
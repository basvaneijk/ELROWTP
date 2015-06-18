using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainMenu : MonoBehaviour {

	public InputField inputLength, inputWidth;
	public GameObject helpFrame, settingsFrame;
	public Button ButtonBlue, ButtonRed, ButtonGreen, MiniGame1, MiniGame2, MiniGame3;
	public Sprite ButtonBlueNormal, ButtonBlueActive, ButtonRedNormal, ButtonRedActive, ButtonGreenNormal, ButtonGreenActive, MiniGame1Normal, MiniGame1Active, MiniGame2Normal, MiniGame2Active, MiniGame3Normal, MiniGame3Active;
	
	/**
	*	Check the "PlayerPrefs" and set the corresponding sprites.
	*/
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
	
	/**
	*	Toggle the help screen. The help screen is used to inform the player of the possibilities.
	*/
	public void toggleHelp() {
		if (helpFrame.activeSelf == true) {
			helpFrame.SetActive(false);
		} else {
			settingsFrame.SetActive(false);
			helpFrame.SetActive(true);
		}
	}
	
	/**
	*	Toggle the settings screen. The settings screen is used to input the dimensios of the room.
	*/
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
	
	/**
	*	Save the input from the settings screen. Saves only on submit.
	*/
	public void saveSettings() {
		PlayerPrefs.SetFloat("width", float.Parse(inputWidth.text));
		PlayerPrefs.SetFloat("length", float.Parse(inputLength.text));
		toggleSettings();
	}
	
	/**
	*	Start the chosen game. The type of game is stored in "PlayerPrefs". It also sets the menu in "PlayerPrefs" where it came from.
	*/
	public void startGame() {
		//if (PlayerPrefs.GetString ("color") && PlayerPrefs.GetString ("game") && PlayerPrefs.GetFloat ("length") && PlayerPrefs.GetFloat ("width")) {
		PlayerPrefs.SetString("menu", "main");	
		Application.LoadLevel(PlayerPrefs.GetString("game"));
		//} else {
		//	Debug.Log ("Niet alle velden zijn correct ingevuld!");
		//}
	}
	
	/**
	*	End the game. Only works when build.
	*/
	public void endGame() {
		Application.Quit();
	}
	
	/**
	*	Set the player color to blue. It also sets the corresponding sprites.
	*/
	public void setColorBlue() {
		PlayerPrefs.SetString("color", "blue");
		ButtonBlue.image.sprite = ButtonBlueActive;
		ButtonRed.image.sprite = ButtonRedNormal;
		ButtonGreen.image.sprite = ButtonGreenNormal;
	}
	
	/**
	*	Set the player color to red. It also sets the corresponding sprites.
	*/
	public void setColorRed() {
		PlayerPrefs.SetString("color", "red");
		ButtonBlue.image.sprite = ButtonBlueNormal;
		ButtonRed.image.sprite = ButtonRedActive;
		ButtonGreen.image.sprite = ButtonGreenNormal;
	}
	
	/**
	*	Set the player color to green. It also sets the corresponding sprites.
	*/
	public void setColorGreen() {
		PlayerPrefs.SetString("color", "green");
		ButtonBlue.image.sprite = ButtonBlueNormal;
		ButtonRed.image.sprite = ButtonRedNormal;
		ButtonGreen.image.sprite = ButtonGreenActive;
	}
	
	/**
	*	Set the game to mini game 1 (Coin Collector). It also sets the corresponding sprites.
	*/
	public void setMiniGame1() {
		PlayerPrefs.SetString("game", "miniGame1Menu");
		MiniGame1.image.sprite = MiniGame1Active;
		MiniGame2.image.sprite = MiniGame2Normal;
		MiniGame3.image.sprite = MiniGame3Normal;
	}
	
	/**
	*	Set the game to mini game 2 (Eten bezorgen). It also sets the corresponding sprites.
	*/
	public void setMiniGame2() {
		PlayerPrefs.SetString("game", "miniGame1");
		MiniGame1.image.sprite = MiniGame1Normal;
		MiniGame2.image.sprite = MiniGame2Active;
		MiniGame3.image.sprite = MiniGame3Normal;
	}
	
	/**
	*	Not applicable!?
	*/
	public void setMiniGame3() {
		PlayerPrefs.SetString("game", "miniGame1");
		MiniGame1.image.sprite = MiniGame1Normal;
		MiniGame2.image.sprite = MiniGame2Normal;
		MiniGame3.image.sprite = MiniGame3Active;
	}

}
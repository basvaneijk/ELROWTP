using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour{
	AsyncOperation async;
	Button exit;
	Button newgame; 
	void Awake(){

	}
	void Start ()
	{
		exit=GameObject.Find ("ExitButton").GetComponent<Button>();
		newgame=GameObject.Find ("NewGameButton").GetComponent<Button>();
		async = Application.LoadLevelAsync("Level1");
		async.allowSceneActivation = false;
		newgame.onClick.AddListener(() => {NewGame();});
		exit.onClick.AddListener(() => {ExitGame();});
	}

	
	private void NewGame() {
		async.allowSceneActivation = true;
	}
	private void ExitGame() {
		Application.Quit();
	}
}
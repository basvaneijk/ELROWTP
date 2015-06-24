using UnityEngine;
using System.Collections;

public class gameMenu : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	/**
	*	Determines to which menu it should return to. Check the "PlayerPrefs" for the corresponding menu. 
	*/
	public void returnToMenu() {
		Application.LoadLevel(PlayerPrefs.GetString("menu"));
	}

    public void MenuView()
    {
        Application.LoadLevel(1);
    }

}
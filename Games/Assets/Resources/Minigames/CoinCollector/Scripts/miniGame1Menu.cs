﻿/*
    CoinCollector game for ELRO Wants To Play
    Copyright (C) 2015 Wouter Janssen & Ben Meulenberg
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class miniGame1Menu : MonoBehaviour
{

	public GameObject helpFrame;
	public Button CoinsLevel1, CoinsLevel2, CoinsLevel3;
	public Sprite CoinsLevel1Normal, CoinsLevel1Lock, CoinsLevel2Normal, CoinsLevel2Lock, CoinsLevel3Normal, CoinsLevel3Lock;

	/**
    *	Check the "PlayerPrefs" and set the corresponding sprites. Also disables the levels that are not achieved.
    */
	void Start ()
	{
		int levelCleared = PlayerPrefs.GetInt ("levelCleared");
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
   /**
    *	Checks every frame the "PlayerPrefs" and set the corresponding sprites if needed. Also disables the levels that are not achieved.
    */
	void Update ()
	{
		if (PlayerPrefs.GetInt ("levelCleared") != PlayerPrefs.GetInt ("level")) {

			int levelCleared = PlayerPrefs.GetInt ("levelCleared");
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
		} else {
			CoinsLevel2.interactable = false;
		}


		if (Input.GetKey (KeyCode.R)) {
			PlayerPrefs.SetInt ("levelCleared", 0);
		}

	}

	/**
    *	Toggle the help screen. The help screen is used to inform the player of the possibilities.
    */
	public void toggleHelp ()
	{
		if (helpFrame.activeSelf == true) {
			helpFrame.SetActive (false);
		} else {
			helpFrame.SetActive (true);
		}
	}
	public void OnEnable ()
	{
		int numberOfGames = 4;
		for (int i = 1; i < numberOfGames; i++) {
			int GameScore = PlayerPrefs.GetInt ("GameScore" + (i - 1));
			Text g = GameObject.Find ("Canvas/GamesFrame/CoinsLevel" + i + "/Text").GetComponent<Text> ();
			if (GameScore != 0 && g.text != SecondsToHhMmSs (new TimeSpan (GameScore))) {
				TimeSpan duration = new TimeSpan (GameScore);
				g.text = SecondsToHhMmSs (duration);
			} else if (GameScore == 0) {
				g.text = "Geen score";
			}
		}
	}
	/**
    *	Set the type of level.
    *	\param level The level to be loaded.
    */
	public void startCoinsLevel (int level)
	{
		PlayerPrefs.SetString ("menu", "miniGame1Menu");
		PlayerPrefs.SetInt ("level", level);
		Application.LoadLevel ("miniGame1");
	}

	/**
    *	Change the scene back to "main".
    */
	public void ReturnToMain ()
	{
		GameObject gameManager = GameObject.FindGameObjectWithTag ("MainMenu_GameManager");
		gameManager.GetComponent<GameManager> ().BackToMainMenu ();
	}

	private string SecondsToHhMmSs (TimeSpan myTimeSpan)
	{
		return string.Format ("{0:00}:{1:00}", myTimeSpan.Minutes, myTimeSpan.Seconds);
	}

}
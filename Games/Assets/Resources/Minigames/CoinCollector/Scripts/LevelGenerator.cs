/*
	CoinCollector game for ELRO Wants To Play
    Copyright (C) 2015 Wouter Janssen
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LevelGenerator : MonoBehaviour
{
	public ArrayList levels;
	public GameObject StartSign;
	public GameObject coin;
	private Vector3 startPosition;
	private Level CurrentLevel;
	private Vector3 offset;
	public bool canStart;
	private float coinOffset;
	public GameObject endScreen;

    
	/**
	*	initialize of the game. Loads all levels and checks which game needs to start.
	*	Also scales and places startpoint
	*/
	void Start ()
	{
		if (PlayerPrefs.GetInt ("GameScore" + PlayerPrefs.GetInt ("level")) == null) {
			PlayerPrefs.SetInt ("GameScore" + PlayerPrefs.GetInt ("level"), 0);
		}
		GameObject.FindGameObjectWithTag ("StartGameButton").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 1000, 0);
		canStart = false;
		offset = new Vector3 (0, 0f, 0);
		coinOffset = -0.25f;
		levels = new ArrayList ();
		level1 ();
		level2 ();
		level3 ();
		startPosition = new Vector3 (0f, 0.3f, 0f);
		Camera.main.transform.position = startPosition;
		CurrentLevel = (Level)levels [PlayerPrefs.GetInt ("level")];

		Vector3 StartSignPos = (Vector3)CurrentLevel.coinPositions [0] + offset;
		(Instantiate (StartSign,
                     new Vector3 (StartSignPos.x * PlayerPrefs.GetFloat ("width"),
                                 StartSignPos.y,
                                 StartSignPos.z * PlayerPrefs.GetFloat ("length")),
                     StartSign.transform.rotation) as GameObject).transform.parent = GameObject.FindGameObjectWithTag ("LevelPlane").transform;
		CurrentLevel.coinPositions.RemoveAt (0);


	}



	// Update is called once per frame and check if the game can start so the coins can spawn
	void Update ()
	{
		if (Input.GetKey (KeyCode.T)) {
			PlayerPrefs.SetInt ("GameScore" + PlayerPrefs.GetInt ("level"), 0);
			Debug.Log ("reset scores");
		}
		if (canStart) {
			GameObject.FindGameObjectWithTag ("StartGameButton").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 0, 0);
			foreach (Vector3 v in CurrentLevel.coinPositions) {
				(Instantiate (coin,
                             new Vector3 (v.x * PlayerPrefs.GetFloat ("width"),
                                         v.y + coinOffset,
                                         v.z * PlayerPrefs.GetFloat ("length")),
                             coin.transform.rotation)
                    as GameObject).transform.parent = GameObject.FindGameObjectWithTag ("LevelPlane").transform;
			}
			canStart = false;
		}

	}
	/**
	*	scale convert especially for this game
	*	\param float data 
	*/
	float getScale (float data)
	{
		return data / 10;
	}

	/**
	*	starts the game
	*/
	public void startGame ()
	{
		GameObject.FindGameObjectWithTag ("StartGameButton").GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 1000, 0);
		GameObject.FindGameObjectWithTag ("Wheelchair").GetComponent<CoinCollection> ().isStarted = true;



	}
	/**
	*	scale convert especially for this game
	*	\param int ticks datetime ticks 
	*/
	public void stopGame (int ticks)
	{
		endScreen.SetActive (true);
		int GameScore = PlayerPrefs.GetInt ("GameScore" + PlayerPrefs.GetInt ("level"));

		PlayerPrefs.SetInt ("levelCleared", PlayerPrefs.GetInt ("level") + 1);
		TimeSpan duration = new TimeSpan (ticks);

		if (GameScore == 0) {
			PlayerPrefs.SetInt ("GameScore" + PlayerPrefs.GetInt ("level"), ticks);
			GameObject.Find ("Canvas/GameEndScreen/HighScore").GetComponent<Text> ().text = "TopScore!";
		} else if (ticks < GameScore) {
			PlayerPrefs.SetInt ("GameScore" + PlayerPrefs.GetInt ("level"), ticks);
			GameObject.Find ("Canvas/GameEndScreen/HighScore").GetComponent<Text> ().text = "TopScore!";
		} else if (ticks < GameScore) {
			PlayerPrefs.SetInt ("GameScore" + PlayerPrefs.GetInt ("level"), ticks);
			GameObject.Find ("Canvas/GameEndScreen/HighScore").GetComponent<Text> ().text = "TopScore!";
		} else {
			GameObject.Find ("Canvas/GameEndScreen/HighScore").GetComponent<Text> ().text = "Geen TopScore!";
		}


		GameObject.Find ("Canvas/GameEndScreen/EndAmount").GetComponent<Text> ().text = "" + GameObject.FindGameObjectWithTag ("Wheelchair").GetComponent<CoinCollection> ().getCoinCount ();
		GameObject.Find ("Canvas/GameEndScreen/EndTime").GetComponent<Text> ().text = SecondsToHhMmSs (duration);



	}
	/**
	*	instantiate level1
	*/
	private void level1 ()
	{
		ArrayList level1Positions = new ArrayList ();
		level1Positions.Add (new Vector3 (-0.38f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (-0.28f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (-0.20f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (-0.15f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (-0.10f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (-0.05f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.05f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.10f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.15f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.20f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.25f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.30f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.35f, 0f, -0.40f));
		level1Positions.Add (new Vector3 (0.40f, 0f, -0.40f));
		Level level1 = new Level (1, coin, level1Positions);
		levels.Add (level1);
	}
	/**
	*	instantiate level2
	*/
	private void level2 ()
	{
		ArrayList level2Positions = new ArrayList ();
		level2Positions.Add (new Vector3 (-0.38f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0.28f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0.20f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0.15f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0.10f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0.05f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (-0f, 0f, -0.40f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.35f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.30f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.25f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.20f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.15f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.10f));
		level2Positions.Add (new Vector3 (0f, 0f, -0.05f));
		level2Positions.Add (new Vector3 (0f, 0f, -0f));
		Level level2 = new Level (2, coin, level2Positions);
		levels.Add (level2);
	}
	/**
	*	instantiate level3
	*/
	private void level3 ()
	{
		ArrayList level3Positions = new ArrayList ();
		level3Positions.Add (new Vector3 (-0.38f, 0f, -0.40f));
		level3Positions.Add (new Vector3 (-0.28f, 0f, -0.40f));
		level3Positions.Add (new Vector3 (-0.20f, 0f, -0.40f));
		level3Positions.Add (new Vector3 (-0.15f, 0f, -0.35f));
		level3Positions.Add (new Vector3 (-0.10f, 0f, -0.30f));
		level3Positions.Add (new Vector3 (-0.05f, 0f, -0.25f));
		level3Positions.Add (new Vector3 (-0f, 0f, -0.20f));
		level3Positions.Add (new Vector3 (0.05f, 0f, -0.15f));
		level3Positions.Add (new Vector3 (0.10f, 0f, -0.10f));
		level3Positions.Add (new Vector3 (0.15f, 0f, -0.05f));
		level3Positions.Add (new Vector3 (0.20f, 0f, 0f));
		level3Positions.Add (new Vector3 (0.25f, 0f, 0.05f));
		level3Positions.Add (new Vector3 (0.3f, 0f, 0.10f));
		level3Positions.Add (new Vector3 (0.35f, 0f, 0.15f));
		level3Positions.Add (new Vector3 (0.4f, 0f, 0.2f));
		Level level3 = new Level (2, coin, level3Positions);
		levels.Add (level3);
	}
	/**
	*	returns number of coins.
	*/
	public int getCoinCount ()
	{
		return CurrentLevel.coinPositions.Count;
	}
	/**
	*	convert timespan to string with MM:SS format
	*/
	private string SecondsToHhMmSs (TimeSpan myTimeSpan)
	{
		return string.Format ("{0:00}:{1:00}", myTimeSpan.Minutes, myTimeSpan.Seconds);
	}
}
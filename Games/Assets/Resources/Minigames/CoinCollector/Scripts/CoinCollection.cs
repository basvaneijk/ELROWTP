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

public class CoinCollection : MonoBehaviour
{

	// Use this for initialization
	private int Coins;
	public AudioClip collectSound;
	public bool isStarted;
	public float speed = 4f;
	public float rotate = 50f;
	void Start ()
	{
		isStarted = false;
	}
	
	/**
	*	Move script for player if the game is running on pc. And location scripts are removed.
	*/
	void Update ()
	{
		if (Input.GetKey (KeyCode.W)) {
			this.transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * -rotate);
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.Translate (-Vector3.forward * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.Rotate (Vector3.up * Time.deltaTime * rotate);
		}
	}

	/**
	*	trigger on collision to collect the arrow to start the game. And collect the coins.
	*/
	void OnTriggerEnter (Collider other)
	{
        
		if (other.gameObject.tag == "Arrow") {
			GameObject.FindGameObjectWithTag ("GameManager").GetComponent<LevelGenerator> ().canStart = true;
			Destroy (other.gameObject);
		}
		if (isStarted) { 
			if (other.gameObject.tag == "Coin") {
				playPickupSound ();
				Coins++;
				UpdateUi ();
				Destroy (other.gameObject);
				//  Debug.Log(GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().getCoinCount() + " | " + Coins);
				if (Coins == GameObject.FindGameObjectWithTag ("GameManager").GetComponent<LevelGenerator> ().getCoinCount ()) {
					isStarted = false;
					GameObject.FindGameObjectWithTag ("GameManager").GetComponent<LevelGenerator> ()
						.stopGame ((int)GameObject.FindGameObjectWithTag ("Canvas").GetComponent<ScoreTimer> ().GetTicks ());
				}
			}
		}
       
       
	}
	/**
	*	play coin sound 
	*/
	public void playPickupSound ()
	{

		gameObject.GetComponent<AudioSource> ().Play ();

	}
	/**
	*	update UI coins collected
	*/
	public void UpdateUi ()
	{

		GameObject.FindGameObjectWithTag ("UI_CoinCount").GetComponent<Text> ().text = "" + Coins;
	}

	public int getCoinCount ()
	{
		return Coins;
	}
}

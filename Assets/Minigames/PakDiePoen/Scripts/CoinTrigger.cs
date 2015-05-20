using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CoinTrigger : MonoBehaviour
{

	int score;
	Text scoreDisplay;

	void Awake ()
	{
		score = 0;
		scoreDisplay = GameObject.Find ("ScoreDisplay").GetComponent<Text> ();
	}

	void Start ()
	{
		ShowScore ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Coin") {
			score++;
			ShowScore ();
		}
	}

	void ShowScore ()
	{
		scoreDisplay.text = score.ToString ();
	}
}

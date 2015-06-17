using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryConditions : MonoBehaviour
{
	public GameObject otherGameObject;

	public int AmountOfCoinsToWin;
	CoinTrigger cointrigger;
	Text VictoryLabel;
	// Use this for initialization

	void Awake ()
	{
		cointrigger = GetComponent<CoinTrigger> ();
		VictoryLabel = GameObject.Find ("VictoryLabel").GetComponent<Text> ();
	}
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (cointrigger.Score () >= AmountOfCoinsToWin) {
			VictoryLabel.text = "You have won the game!";
		}
	}

}

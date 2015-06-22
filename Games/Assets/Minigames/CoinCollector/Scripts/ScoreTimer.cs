using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreTimer : MonoBehaviour
{
	private DateTime start;

	Text timerText;

	void Start ()
	{
		timerText = GameObject.Find ("Timer").GetComponent<Text> ();
	}

	public void ResetTimer ()
	{
		start = DateTime.Now;
	}

	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Wheelchair").GetComponent<CoinCollection> ().isStarted) {
			TimeSpan duration = DateTime.Now - start;
			timerText.text = duration.Minutes + ":" + duration.Seconds;
		}
	}
	public int GetMinutes ()
	{
		return (DateTime.Now - start).Minutes;
	}

	public long GetSeconds ()
	{
		return (DateTime.Now - start).Seconds;
	}

	public long GetTicks ()
	{
		return (DateTime.Now - start).Ticks;
	}
}
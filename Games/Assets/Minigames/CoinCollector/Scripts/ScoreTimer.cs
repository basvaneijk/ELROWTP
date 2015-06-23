using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreTimer : MonoBehaviour
{
	private DateTime start;

	Text timerText;
	bool startTimer;

	void Start ()
	{
		timerText = GameObject.Find ("Timer").GetComponent<Text> ();
        start = DateTime.Now;
		startTimer = true;
	}

	public void ResetTimer ()
	{
		start = DateTime.Now;
	}

	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Wheelchair").GetComponent<CoinCollection> ().isStarted) {
			if (startTimer) {
				ResetTimer ();
				startTimer = false;
			}
			TimeSpan duration = DateTime.Now - start;
			timerText.text = duration.Minutes + ":" + duration.Seconds;
            Debug.Log(duration.Ticks + " | " + duration.Minutes + ":" + duration.Seconds);
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
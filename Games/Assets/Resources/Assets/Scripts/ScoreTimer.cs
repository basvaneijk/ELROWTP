using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreTimer : MonoBehaviour
{
	private DateTime start;

	Text timerText;
    bool timerStarted;

	void Start ()
	{
		timerText = GameObject.Find ("Timer").GetComponent<Text> ();
        start = DateTime.Now;
        timerStarted = true;
	}

	public void ResetTimer ()
	{
		start = DateTime.Now;
	}

	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Wheelchair").GetComponent<CoinCollection> ().isStarted) {
            if (timerStarted)
            {
                ResetTimer();
                timerStarted = false;
            }
			TimeSpan duration = DateTime.Now - start;
            timerText.text = SecondsToHhMmSs(duration);
           
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
    private string SecondsToHhMmSs(TimeSpan myTimeSpan)
    {
        return string.Format("{0:00}:{1:00}", myTimeSpan.Minutes, myTimeSpan.Seconds);
    }
}
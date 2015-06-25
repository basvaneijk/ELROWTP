/*
    Framework for ELRO Wants To Play
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
			if (timerStarted) {
				ResetTimer ();
				timerStarted = false;
			}
			TimeSpan duration = DateTime.Now - start;
			timerText.text = SecondsToHhMmSs (duration);
           
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
	private string SecondsToHhMmSs (TimeSpan myTimeSpan)
	{
		return string.Format ("{0:00}:{1:00}", myTimeSpan.Minutes, myTimeSpan.Seconds);
	}
}
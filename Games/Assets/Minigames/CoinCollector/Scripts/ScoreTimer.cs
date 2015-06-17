using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour {
	private float minutes = 0f;
	private float seconds = 0f;
	private float milliseconds = 0f;


	private float highScoreMinutes;
	private float highScoreSeconds;
	Text timerText;
	//Text highScoreText;

	// Use this for initialization
	void Start () {
		timerText = GameObject.Find ("Timer").GetComponent<Text> ();
		//highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){
			PlayerPrefs.SetFloat("minutescore", minutes);
				//if(
			PlayerPrefs.SetFloat("secondsscore", seconds);
		}

		/*
		if (Input.GetKeyDown("up")){
			highScoreMinutes = PlayerPrefs.GetFloat("minutescore");
			highScoreSeconds = PlayerPrefs.GetFloat("secondsscore");

			highScoreText.text = highScoreMinutes.ToString ("f0") + highScoreSeconds.ToString ("f0");
		}
		*/
		milliseconds += (Time.deltaTime * 1000);

		if (seconds < 10 && minutes < 10) {
			timerText.text = "0" + minutes.ToString ("f0") + ":0" + seconds.ToString ("f0");
		} else if (seconds <= 9) {
            timerText.text = "0" + minutes.ToString("f0") + ":" + seconds.ToString("f0");
		} else if (minutes <=9){
            timerText.text = "0" + minutes.ToString("f0") + ":" + seconds.ToString("f0");
        } else
        timerText.text = "" + minutes.ToString("f0") + ":" + seconds.ToString("f0") + ":";

   
        if (milliseconds >= 1000)
        {
            seconds += 1;
            milliseconds = 0;
        }

        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0;
        }

	}
}
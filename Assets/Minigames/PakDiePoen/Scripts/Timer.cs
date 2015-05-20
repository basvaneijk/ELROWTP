using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public float minutes = 0f;
	public float seconds = 0f;
	public float milliseconds = 0f;
	Text timerText;


	// Use this for initialization
	void Start () {
		timerText = GameObject.Find ("TimerText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		milliseconds += (Time.deltaTime * 1000);
		if (seconds <= 9 && minutes <= 9) {
			timerText.text = "00" + minutes.ToString ("f0") + ":0" + seconds.ToString ("f0") + ":" + milliseconds.ToString ("f0");
		} else if (seconds <= 9) {
            timerText.text = "" + minutes.ToString("f0") + ":" + seconds.ToString("f0") + ":" + (milliseconds / 10).ToString("f0");
		} else if (minutes <=9){
            timerText.text = "" + minutes.ToString("f0") + ":" + seconds.ToString("f0") + ":" + (milliseconds/10).ToString("f0");
        }
        else
        timerText.text = "" + minutes.ToString("f0") + ":" + seconds.ToString("f0") + ":" + (milliseconds / 10).ToString("f0");



   
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

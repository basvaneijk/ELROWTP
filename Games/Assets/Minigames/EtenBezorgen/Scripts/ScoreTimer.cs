using UnityEngine;
using System.Collections;
using System;

public class ScoreTimer : MonoBehaviour {

    private float score = 15f;
    private float tip = 5f;
    private DateTime time;

	// Use this for initialization
	void Start () {
        time = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
        DateTime tempTime = DateTime.Now;
        if (tip > 0f && (tempTime.Hour > time.Hour || tempTime.Minute > time.Minute || tempTime.Second > time.Second) )
        {
            tip -= 0.20f;
            time = tempTime;
        }
	}

    public float getScore()
    {
        return score + tip;
    }
}

using UnityEngine;
using System.Collections;
using System;

public class tipCounter : MonoBehaviour {

    private float score;
    private float tip;
    private DateTime time;
    private Boolean isStarted = false;
    private DateTime tempTime;
    private int pause = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isStarted)
        {
            tempTime = DateTime.Now;
            if (tip > 0f && (tempTime.Hour > time.Hour || tempTime.Minute > time.Minute || tempTime.Second > time.Second))
            {
                if(pause > 0){
                    pause--;
                }else{
                    tip -= 0.20f;
                    time = tempTime;
                }
            }
        }
	}

    public void startGame()
    {
        time = DateTime.Now;
        isStarted = true;
        if (score == null)
        {
            score = 15f;
        }
        if (tip == null)
        {
            tip = 5f;
        }
    }

    public float getScore()
    {
        return score + tip;
    }

    public void setScore(float points)
    {
        this.score = points;
    }

    public void setTip(float tipmoney)
    {
        this.tip = tipmoney;
    }
}

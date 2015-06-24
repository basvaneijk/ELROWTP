using UnityEngine;
using System.Collections;
using System;

public class tipCounter : MonoBehaviour
{

    private float score;
    private float tip;
    private float maxtip;
    private float tipDecrease;
	private DateTime time;
	private Boolean isStarted = false;
	private DateTime tempTime;
    private int pause;
    private float totalScore;

	// Use this for initialization
	void Start ()
    {
        totalScore = 0f;
        pause = 30;
        score = 15f;
        tip = 5f;
        tipDecrease = 0.20f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isStarted)
        {
            tempTime = DateTime.Now;
            if (pause > 0 && (tempTime.Subtract(time).TotalSeconds > 1))
            {
                pause -= 1;
                time = tempTime;
            }
            else if(tip > 0.1f && tempTime.Subtract(time).TotalSeconds > 2)
            {
                if (tip < (tipDecrease + 0.1f))
                {
                    tip = 0.0f;
                }
                else
                {
                    tip -= tipDecrease;
                }
                time = tempTime;
            }
        }
    }

	public void startCounter ()
	{
		time = DateTime.Now;
		isStarted = true;
        maxtip = tip;
	}

    public void stopCounter()
    {
        isStarted = false;
        totalScore += this.getScore();
        pause = 30;
        score = 15f;
        tip = 5f;
        tipDecrease = 0.20f;
    }

	public float getScore ()
	{
		return score + tip;
	}

    public void SetValues(float score, float tip, float tipDecrease,int pause)
    {
        this.score = score;
        this.tip = tip;
        this.tipDecrease = tipDecrease;
        this.pause = pause;
    }

    public float getTip()
    {
        return tip;
    }

    public float getMaxtip()
    {
        return maxtip;
    }

    public float getTipDecrease()
    {
        return tipDecrease;
    }

    public Boolean started()
    {
        return isStarted;
    }

    public float getTotalScore()
    {
        return totalScore;
    }
}

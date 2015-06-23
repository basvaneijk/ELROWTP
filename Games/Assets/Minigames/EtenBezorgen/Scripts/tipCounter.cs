using UnityEngine;
using System.Collections;
using System;

public class tipCounter : MonoBehaviour
{

    private float score;
    private float tip;
    private float maxtip;
	private DateTime time;
	private Boolean isStarted = false;
	private DateTime tempTime;
	private int pause = 30;

	// Use this for initialization
	void Start ()
    {
        score = 15f;
        tip = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isStarted)
        {
            tempTime = DateTime.Now;
            if (tip > 0f && (tempTime.Subtract(time).TotalSeconds > 0))
            {
                if(pause > 0){
                    pause--;
                }else{
                    if (tip < 0.20)
                    {
                        tip = 0.0f;
                    }
                    else
                    {
                        tip -= 0.20f;
                    }
                    time = tempTime;
                }
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
        score = 15f;
        tip = 5f;
    }

	public float getScore ()
	{
		return score + tip;
	}

	public void setScore (float points)
	{
		this.score = points;
	}

    public void setTip(float tipmoney)
    {
        this.tip = tipmoney;
    }

    public float getTip()
    {
        return tip;
    }

    public float getMaxtip()
    {
        return maxtip;
    }
}

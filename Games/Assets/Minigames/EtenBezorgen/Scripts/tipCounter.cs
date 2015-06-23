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
            if (pause > 0 && (tempTime.Subtract(time).TotalSeconds > 1))
            {
                Debug.Log(pause);
                pause -= 1;
                time = tempTime;
            }
            else if(tip > 0.1f && tempTime.Subtract(time).TotalSeconds > 2)
            {
                if (tip < 0.30f)
                {
                    Debug.Log(tip);
                    tip = 0.0f;
                    Debug.Log(tip);
                }
                else
                {
                    Debug.Log(tip);
                    tip -= 0.20f;
                    Debug.Log(tip);
                }
                time = tempTime;
            }
        }
    }


    /*tempTime = DateTime.Now;
            /if (pause > 0 && (tempTime.Subtract(time).TotalSeconds > 1))
            {
                pause--;
            }
            else
            {
                if (pause <= 0 && tip > 0.1f && (tempTime.Subtract(time).TotalSeconds > 5))
                {
                    if (tip < 0.20f)
                    {
                        Debug.Log(tip);
                        tip = 0.0f;
                        Debug.Log(tip);
                    }
                    else
                    {
                        Debug.Log(tip);
                        tip -= 0.20f;
                        Debug.Log(tip);
                    }
                }
              }*
                time = tempTime;
           }*/

	public void startCounter ()
	{
		time = DateTime.Now;
		isStarted = true;
        maxtip = tip;
	}

    public void stopCounter()
    {
        isStarted = false;
        pause = 30;
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

    public Boolean started()
    {
        return isStarted;
    }
}

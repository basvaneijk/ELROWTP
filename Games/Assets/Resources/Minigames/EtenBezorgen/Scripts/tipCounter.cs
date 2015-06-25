/*
    TipCounter for MiniGame2 - ELRO Wants To Play
    Copyright (C) 2015 Jan-Willem Hoekman

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


    /**
     * Start the tipcounter
     */
	public void startCounter ()
	{
		time = DateTime.Now;
		isStarted = true;
        maxtip = tip;
	}

    /**
     * stop and reset the tipcounter
     */
    public void stopCounter()
    {
        isStarted = false;
        totalScore += this.getScore();
        pause = 30;
        score = 15f;
        tip = 5f;
        tipDecrease = 0.20f;
    }

    /**
     * Set the score variables for the next delivery
     * \param score this the desired score upon delivery 
     * \param tip this is the etra score for completing the delivery quicker
     * \param tipDecrease this is the value with wich the tip decreases every 2 seconds
     * \param pause this is the time in seconds the counter waits before starting to decrease the tip
     */
    public void SetValues(float score, float tip, float tipDecrease, int pause)
    {
        this.score = score;
        this.tip = tip;
        this.tipDecrease = tipDecrease;
        this.pause = pause;
    }

    /**
     * Get the current score of the tipcounter
     * \return score
     */
	public float getScore ()
	{
		return score + tip;
	}

    /**
     * Get current tip
     * \return tip
     */
    public float getTip()
    {
        return tip;
    }

    /**
     * Get the maximum tip
     * \return maxTip
     */
    public float getMaxtip()
    {
        return maxtip;
    }

    /**
     * Get the tipDecrease amount
     * \return tipDecrease
     */
    public float getTipDecrease()
    {
        return tipDecrease;
    }

    /**
     * Get a bool of wether teh counter is started or not
     * \return isStarted
     */
    public Boolean started()
    {
        return isStarted;
    }

    /**
     * Get the total score for the player
     * \return totalScore
     */
    public float getTotalScore()
    {
        return totalScore;
    }
}

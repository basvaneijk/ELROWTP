/*
    HudHandler for MiniGame2 - ELRO Wants To Play
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
using UnityEngine.UI;
using System;

public class hudHandler : MonoBehaviour {

    public RectTransform TipTransform;
    private float minXValue;
    private float maxXValue;
    public Text tipText;
    public Text totalScoreText;
    public GameObject kitchen;
    public GameObject tipHUD;
	// Use this for initialization
    void Start()
    {
        maxXValue = TipTransform.position.x;
        minXValue = TipTransform.position.x - TipTransform.rect.width;
	}
	
	// Update is called once per frame
    void Update()
    {
        totalScoreText.text = "€" + kitchen.GetComponent<tipCounter>().getTotalScore().ToString("0.00");
        if (!tipHUD.activeInHierarchy && kitchen.GetComponent<tipCounter>().started())
        {
            
            tipHUD.SetActive(true);
        }
        else if (tipHUD.activeInHierarchy && !kitchen.GetComponent<tipCounter>().started())
        {
            tipHUD.SetActive(false);
        }
        if (kitchen.GetComponent<tipCounter>().started())
        {
            tipText.text = "Fooi: €" + kitchen.GetComponent<tipCounter>().getTip().ToString("0.00");
            
            
            if (kitchen.GetComponent<tipCounter>().getTip() < 0.10F)
            {
                TipTransform.position = new Vector3(minXValue, TipTransform.position.y);
            }
            else if (kitchen.GetComponent<tipCounter>().getMaxtip() != kitchen.GetComponent<tipCounter>().getTip())
            {
                //determain the new position of the tipbar depending on the lost tip
                TipTransform.position = new Vector3(maxXValue - (
                
                //get the bar with and devide it by the max tip
                (TipTransform.rect.width / kitchen.GetComponent<tipCounter>().getMaxtip())
                * kitchen.GetComponent<tipCounter>().getTipDecrease()) *

                //devide the maxtip by the tipdecrease and divede that by the maxtip devided by the lost tip
                (
                (kitchen.GetComponent<tipCounter>().getMaxtip() / kitchen.GetComponent<tipCounter>().getTipDecrease()) /
                (kitchen.GetComponent<tipCounter>().getMaxtip() / 
                (kitchen.GetComponent<tipCounter>().getMaxtip() - kitchen.GetComponent<tipCounter>().getTip())))
                
                , TipTransform.position.y);
            }
            else
            {
                TipTransform.position = new Vector3(maxXValue, TipTransform.position.y);
            }
        }
    }
}

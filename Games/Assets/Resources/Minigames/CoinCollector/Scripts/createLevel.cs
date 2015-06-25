/*
    CoinCollector game for ELRO Wants To Play
    Copyright (C) 2015 Ben Meulenberg
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

public class createLevel : MonoBehaviour
{

	float width;
	float length;

	/**
	*	Check the "PlayerPrefs" and set the scale of the plane according to the dimensions.
	*/
	void Start ()
	{

		if (PlayerPrefs.GetFloat ("width") > 0 && PlayerPrefs.GetFloat ("length") > 0) {
			GameObject.FindGameObjectWithTag ("LevelPlane").gameObject.transform.localScale = new Vector3 (getScale (PlayerPrefs.GetFloat ("width")), 0.1F, getScale (PlayerPrefs.GetFloat ("length")));
		}
	
	}
	
	void Update ()
	{

		/*if (Input.GetKeyDown("1")) {

			width = 10;
			length = 10;

			PlayerPrefs.SetFloat("width", width);
			PlayerPrefs.SetFloat("length", length);

            GameObject.FindGameObjectWithTag("LevelPlane").gameObject.transform.localScale = new Vector3(getScale(width), 0.1F, getScale(length));
		}

		if (Input.GetKeyDown("2")) {

			width = 100;
			length = 100;

			PlayerPrefs.SetFloat("width", width);
			PlayerPrefs.SetFloat("length", length);

            GameObject.FindGameObjectWithTag("LevelPlane").gameObject.transform.localScale = new Vector3(getScale(width), 0.1F, getScale(length));
		}*/
	
	}
	
	/**
	*	Get the scale. 
	*	\param data The length or width (dimensions)
	*	\return The data divided by 10 (scaling)
	*/
	float getScale (float data)
	{
		return data / 10;
	}

}

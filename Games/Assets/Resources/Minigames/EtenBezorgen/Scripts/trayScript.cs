/*
    TrayScript for MiniGame2 - ELRO Wants To Play
    Copyright (C) 2015 Jelmer Bootsma

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

public class trayScript : MonoBehaviour {
	public GameObject trayModel;
	public GameObject[] foodModels;
	private int currentFood = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	/**
     * Updates every frame, sets the correct position to the model on the tray.
     */
	void Update () {
		// debug \\ int a = Random.Range (0, foodModels.Length-1);
		trayModel.transform.position = transform.position;
		trayModel.transform.rotation = transform.rotation;
	}
	/**
     * Clears the tray by setting the model to inactive.
     */
	public void clearTray(){
		trayModel.SetActive (false);
	}
	/**
     * Set the correct foodmodel on the tray.
     * \param a The foodID taken from the array of foodmodels. 
     */
	public void setFood(int a){
		currentFood = a;
		GameObject tmp = Instantiate (foodModels [a], trayModel.transform.position, trayModel.transform.rotation) as GameObject;
		tmp.transform.localScale /= 4;
		trayModel.SetActive (false);
		trayModel = tmp;
		trayModel.SetActive (true);
	}
	/**
     * Get the foodID from the tray.
     * \return Returns the foodID of the food on the tray.
     */
	public int getCurrentFood(){
		return currentFood;

	}
}

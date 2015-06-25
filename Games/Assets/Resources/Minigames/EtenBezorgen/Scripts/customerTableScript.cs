/*
    CustomerTableScript for MiniGame2 - ELRO Wants To Play
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

public class customerTableScript : MonoBehaviour {

	public GameObject[] foodModels;
	public GameObject foodOnTableModel;
	public GameObject kitchenObject;
	public GameObject presentTray;
	private int requestedFood = 0;
	private bool waitingForFood = false;
	private float requestTimer = 5;
    private int foodCount = 6;
    public GameObject kitchen;	

	// Use this for initialization
	void Start () {
        GameObject.Find("kitchen");
	}
	
	// Update is called once per frame
	void Update () {
		if (!waitingForFood) {
			requestTimer -= Time.deltaTime;
			if(requestTimer <0){
				orderFood();
			}
		}
	}
	/**
     * Allows the customer to request food from the foodArray from the kitchen.
	 */
	private void orderFood(){
			waitingForFood = true;
		// model show
		requestedFood = Random.Range (0,foodCount -1);
		GameObject tmp = Instantiate (foodModels [requestedFood], foodOnTableModel.transform.position, foodOnTableModel.transform.rotation) as GameObject;
		Destroy (foodOnTableModel);
		foodOnTableModel.SetActive (true);
		foodOnTableModel = tmp;
		foodOnTableModel.transform.localScale /= 5;
		// call for kitchen chef
		kitchenObject.GetComponent<kitchenScript>().requestFood (requestedFood);
	} 

	void OnTriggerEnter (Collider col)
	{
		print ("Collision with: " + col.gameObject.name);
		if(col.gameObject.name == "DishWood"){
			int givenFood = presentTray.GetComponent<trayScript>().getCurrentFood(); //cookingRequests.Dequeue()
			if(givenFood == requestedFood){
				// delivery
                kitchen.GetComponent<tipCounter>().stopCounter();
				foodOnTableModel.SetActive (false);
				presentTray.GetComponent<trayScript>().clearTray();
				waitingForFood = false;
				requestedFood = -1;
				requestTimer = Random.Range(10,15);
			}else{
				// wrong

			}
		}
	}
}

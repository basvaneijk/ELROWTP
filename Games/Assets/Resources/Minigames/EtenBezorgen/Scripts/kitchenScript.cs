/*
    KitchenScript for MiniGame2 - ELRO Wants To Play
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
using System.Timers;
using System.Collections.Generic;

public class kitchenScript : MonoBehaviour {
	private int food;
	private bool foodReady = false;
	private float foodInterval = 5;
	private float timer = 0;
	public GameObject[] foodModels;
	private int foodCount = 0;
	private int presentedFood = 0;
	public GameObject foodOnTableModel;
	public GameObject presentTray;
	private bool preparingFood = false;
	private Queue<int> cookingQueue = new Queue<int>();


	// Use this for initialization
	void Start () {
		timer = foodInterval;
		foodCount = foodModels.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (preparingFood) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				// food ready
				prepareFood ();
			} else {
			
			}
		}

	}
	/**
     * Use this function to add a new foodRequest to the Queue.
     * \param i The foodID of the food that the clients wants. 
     */
	public void requestFood(int i){
		// Q
		cookingQueue.Enqueue (i);
		preparingFood = true;
	}
	/**
     * Prepares the food if there is no food present on the kitchencounter.
	 */
	void prepareFood(){
		if (!foodReady) {
			foodOnTableModel.SetActive(true);
			print("Food Ready for pickup!");
			this.GetComponent<audioBehavior>().playsound(0);
			foodReady = true;
			timer = foodInterval;
			presentFood();
		}
	}
	/**
     * Presents the food if there is no food present on the kitchencounter.
	 */
	void presentFood(){
		// debug  \\ presentedFood = Random.Range (0,foodCount -1); // deQ
		presentedFood = cookingQueue.Dequeue ();
		GameObject tmp = Instantiate (foodModels [presentedFood], foodOnTableModel.transform.position, foodOnTableModel.transform.rotation) as GameObject;
		Destroy (foodOnTableModel);
		foodOnTableModel = tmp;
		foodOnTableModel.transform.localScale /= 5;
	}
	void OnCollisionEnter(Collision col){
		print (col);
	}
	void OnTriggerEnter (Collider col)
	{
		//print ("Collision with: " + col.gameObject.name);
		if(col.gameObject.name == "waiter")
		{
			if(foodReady){
				presentTray.GetComponent<trayScript>().setFood(presentedFood); //cookingRequests.Dequeue()
				foodReady = false;
				timer = foodInterval;
				foodOnTableModel.SetActive (false);
                this.GetComponent<tipCounter>().startCounter();
				if(cookingQueue.Count == 0){
					preparingFood = false;

				}else{
					preparingFood = true;
					timer = foodInterval;
				}
			}
			
			
		}
	}
}

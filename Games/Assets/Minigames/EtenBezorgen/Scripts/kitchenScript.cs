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
	private GameObject foodOnTableModel;
	// Use this for initialization
	void Start () {
		timer = foodInterval;
		foodCount = foodModels.Length;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			// food ready
			prepareFood();
		} else {
			
		}
	}
	
	void prepareFood(){
		if (!foodReady) {
			print("Food Ready for pickup!");
			foodReady = true;
			timer = foodInterval;
			presentFood();
		}
	}
	void presentFood(){
		presentedFood = Random.Range (0,foodCount -1);
		GameObject tmp = Instantiate (foodModels [presentedFood], foodOnTableModel.transform.position, foodOnTableModel.transform.rotation) as GameObject;
		Destroy (foodOnTableModel);
		foodOnTableModel = tmp;
	}
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "waiter")
		{
			if(foodReady){
				GameObject a = col.gameObject;
				//a.GetComponent<waiter_plate>().giveFood(presentedFood); //cookingRequests.Dequeue()
				foodReady = false;
				timer = foodInterval;
			}
			
			
		}
	}
}

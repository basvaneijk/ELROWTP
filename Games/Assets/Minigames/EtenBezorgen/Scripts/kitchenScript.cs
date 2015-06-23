
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
	public void requestFood(int i){
		// Q
		cookingQueue.Enqueue (i);
		preparingFood = true;
	}
	
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
	void presentFood(){
		// debug  \\ presentedFood = Random.Range (0,foodCount -1); // deQ
		presentedFood = cookingQueue.Dequeue ();
		GameObject tmp = Instantiate (foodModels [presentedFood], foodOnTableModel.transform.position, foodOnTableModel.transform.rotation) as GameObject;
		Destroy (foodOnTableModel);
		foodOnTableModel = tmp;
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

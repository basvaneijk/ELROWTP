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
	private void orderFood(){
			waitingForFood = true;
		// model show
		requestedFood = Random.Range (0,foodCount -1);
		GameObject tmp = Instantiate (foodModels [requestedFood], foodOnTableModel.transform.position, foodOnTableModel.transform.rotation) as GameObject;
		Destroy (foodOnTableModel);
		foodOnTableModel.SetActive (true);
		foodOnTableModel = tmp;
		// call for kitchen chef
		kitchenObject.GetComponent<kitchenScript>().requestFood (requestedFood);
	} 

	void OnTriggerEnter (Collider col)
	{
		print ("Collision with: " + col.gameObject.name);
		if(col.gameObject.name == "waiter"){
			int givenFood = presentTray.GetComponent<trayScript>().getCurrentFood(); //cookingRequests.Dequeue()
			if(givenFood == requestedFood){
				// delivery
                kitchen.GetComponent<tipCounter>().stopCounter();
				foodOnTableModel.SetActive (false);
				presentTray.GetComponent<trayScript>().clearTray();
				waitingForFood = false;
				requestTimer = Random.Range(10,15);
			}else{
				// wrong

			}
		}
	}
}

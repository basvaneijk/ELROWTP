using UnityEngine;
using System.Collections;

public class trayScript : MonoBehaviour {
	public GameObject trayModel;
	public GameObject[] foodModels;
	private int currentFood = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// debug \\ int a = Random.Range (0, foodModels.Length-1);
		trayModel.transform.position = transform.position;
		trayModel.transform.rotation = transform.rotation;
	}
	public void setFood(int a){
		currentFood = a;
		GameObject tmp = Instantiate (foodModels [a], trayModel.transform.position, trayModel.transform.rotation) as GameObject;
		tmp.transform.localScale /= 3;
		trayModel.SetActive (false);
		trayModel = tmp;
		trayModel.SetActive (true);
	}
	public int getFood(){
		return currentFood;

	}
}

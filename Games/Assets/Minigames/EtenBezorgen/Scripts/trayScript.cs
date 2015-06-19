using UnityEngine;
using System.Collections;

public class trayScript : MonoBehaviour {
	public GameObject trayModel;
	public GameObject[] foodModels;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// debug \\ int a = Random.Range (0, foodModels.Length-1);

	}
	public void setFood(int a){
		GameObject tmp = Instantiate (foodModels [a], trayModel.transform.position, trayModel.transform.rotation) as GameObject;
		Destroy (trayModel);
		trayModel = tmp;
	}
}

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int levelNumber;
	public GameObject collectable;
	public ArrayList coinPositions = new ArrayList();

	/**
	*	Create level object
	*	\param int levelNum level number
	*	\param GameObject coin prefab coin
	*	\param ArrayList coinsPos arraylist with coin positions
	*/
	public Level(int levelNum, GameObject coin, ArrayList coinsPos){
		levelNumber = levelNum;
		collectable = coin;
		coinPositions = coinsPos;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

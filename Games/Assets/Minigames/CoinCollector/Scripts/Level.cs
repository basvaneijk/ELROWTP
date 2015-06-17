using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int levelNumber;
	public GameObject collectable;
	public ArrayList coinPositions = new ArrayList();

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

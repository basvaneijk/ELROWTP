using UnityEngine;
using System.Collections;


public class Levels: MonoBehaviour {
	public ArrayList levels;
	public GameObject coin;
	// Use this for initialization
	void Start () {
		levels = new ArrayList();
		coin = (GameObject)Resources.Load ("Assets/Coin");
		level1 ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void level1(){
		ArrayList level1Positions = new ArrayList();
		level1Positions.Add (new Vector3 (1f, 0f, 1f));
		level1Positions.Add (new Vector3 (5f, 0f, 1f));
		level1Positions.Add (new Vector3 (10f, 0f, 1f));
		level1Positions.Add (new Vector3 (15f, 0f, 1f));
		level1Positions.Add (new Vector3 (20f, 0f, 1f));
		level1Positions.Add (new Vector3 (25f, 0f, 1f));
		level1Positions.Add (new Vector3 (30f, 0f, 1f));
		level1Positions.Add (new Vector3 (35f, 0f, 1f));
		level1Positions.Add (new Vector3 (40f, 0f, 1f));
		level1Positions.Add (new Vector3 (45f, 0f, 1f));
		level1Positions.Add (new Vector3 (50f, 0f, 1f));
		level1Positions.Add (new Vector3 (55f, 0f, 1f));
		level1Positions.Add (new Vector3 (60f, 0f, 1f));
		level1Positions.Add (new Vector3 (65f, 0f, 1f));
		level1Positions.Add (new Vector3 (70f, 0f, 1f));
		Level level1 = new Level(1,coin,level1Positions);
		levels.Add (level1);
	}
	public ArrayList getAll(){
		return levels;
	}
}

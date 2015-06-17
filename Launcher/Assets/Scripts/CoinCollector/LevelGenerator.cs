using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
    public ArrayList levels;
    public GameObject StartSign;
    public GameObject coin;
	private Vector3 startPosition;
    private Level CurrentLevel;
    private Vector3 offset;
    public bool canStart;
    private float coinOffset;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("StartGameButton").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1000, 0);
        canStart = false;
        offset = new Vector3(0, 0.6f, 0);
        coinOffset = 0.30f;
        levels = new ArrayList();
        level1();
		startPosition = new Vector3(0f,1f,0f);
		Camera.main.transform.position = startPosition;
		CurrentLevel = (Level)levels[PlayerPrefs.GetInt("level")];

        Vector3 StartSignPos = (Vector3)CurrentLevel.coinPositions[0] + offset;
        (Instantiate(StartSign, new Vector3(StartSignPos.x * PlayerPrefs.GetFloat("width"), StartSignPos.y, StartSignPos.z * PlayerPrefs.GetFloat("length")), StartSign.transform.rotation) as GameObject).transform.parent = GameObject.FindGameObjectWithTag("LevelPlane").transform;
        CurrentLevel.coinPositions.RemoveAt(0);
        
		
	}

	

	// Update is called once per frame
	void Update () {
        if (canStart)
        {
            GameObject.FindGameObjectWithTag("StartGameButton").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            foreach (Vector3 v in CurrentLevel.coinPositions)
            {
                (Instantiate(coin, new Vector3(v.x * PlayerPrefs.GetFloat("width"), v.y + coinOffset, v.z * PlayerPrefs.GetFloat("length")), coin.transform.rotation) as GameObject).transform.parent = GameObject.FindGameObjectWithTag("LevelPlane").transform;
            }
            canStart = false;
        }
        
	}

    float getScale(float data)
    {
        return data / 10;
    }

    public void startGame()
    {
        GameObject.FindGameObjectWithTag("StartGameButton").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1000, 0);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CoinCollection>().isStarted = true;
        
        // timer start
      
    }
    private void level1()
    {
        ArrayList level1Positions = new ArrayList();
        level1Positions.Add(new Vector3(-0.38f, 0f, -0.40f));
        level1Positions.Add(new Vector3(-0.28f, 0f, -0.40f));
        level1Positions.Add(new Vector3(-0.20f, 0f, -0.40f));
        level1Positions.Add(new Vector3(-0.15f, 0f, -0.40f));
        level1Positions.Add(new Vector3(-0.10f, 0f, -0.40f));
        level1Positions.Add(new Vector3(-0.05f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.05f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.10f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.15f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.20f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.25f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.30f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.35f, 0f, -0.40f));
        level1Positions.Add(new Vector3(0.40f, 0f, -0.40f));
        Level level1 = new Level(1,coin, level1Positions);
        levels.Add(level1);
    }
}

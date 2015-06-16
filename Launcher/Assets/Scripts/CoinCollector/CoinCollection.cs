using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCollection : MonoBehaviour {

	// Use this for initialization
    private int Coins;
    public AudioClip collectSound;
    public bool isStarted;

	void Start () {
        isStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "arrow")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().canStart = true;
            Destroy(other.gameObject);
        }
        if (isStarted) { 
           if (other.gameObject.tag == "coin")
        {
            playPickupSound();
            Coins++;
            // UpdateUi();
            Destroy(other.gameObject);
        }
        }
       
       
    }
    public void playPickupSound()
    {

        gameObject.GetComponent<AudioSource>().Play();

    }

    public void UpdateUi()
    {

        GameObject.FindGameObjectWithTag("UI_CoinCount").GetComponent<Text>().text = "" + Coins;

    }


}

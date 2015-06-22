using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinCollection : MonoBehaviour {

	// Use this for initialization
    private int Coins;
    public AudioClip collectSound;
    public bool isStarted;
    public float speed = 4f;
    public float rotate = 50f;
	void Start () {
        isStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * -rotate);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * rotate);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Arrow")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().canStart = true;
            Destroy(other.gameObject);
        }
        if (isStarted) { 
           if (other.gameObject.tag == "Coin")
        {
            playPickupSound();
            Coins++;
            UpdateUi();
            Destroy(other.gameObject);
             //  Debug.Log(GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().getCoinCount() + " | " + Coins);
            if (Coins == GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().getCoinCount())
            {
                isStarted = false;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelGenerator>().stopGame(GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScoreTimer>().GetSeconds(), GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScoreTimer>().GetMinutes());
            }
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
    public int getCoinCount()
    {
        return Coins;
    }


}

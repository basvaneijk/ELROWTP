using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class audioBehavior : MonoBehaviour {


    private AudioSource audiosource;
    public List<AudioClip> soundFiles = new List<AudioClip>();

	// Use this for initialization
	void Start () {
        audiosource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playsound(int ind)
    {
        if(ind < soundFiles.Count){
            audiosource.clip = soundFiles[ind];
            audiosource.Play();
        }
    }
}

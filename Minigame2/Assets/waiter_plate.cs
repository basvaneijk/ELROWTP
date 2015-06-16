using UnityEngine;
using System.Collections;

public class waiter_plate : MonoBehaviour {
	GameObject waiter;
	Renderer waiter_renderer;
	public Texture sandwich;
	public Texture cake;
	public Texture ice;
	// Use this for initialization
	void Start () {
		waiter = GameObject.Find ("dienblad");
		waiter_renderer = waiter.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void pickup(int a){
		switch (a) {
		case 1:
			waiter_renderer.material.mainTexture = sandwich;
			break;
		case 2:
			waiter_renderer.material.mainTexture = cake;
			break;
		case 3:
			waiter_renderer.material.mainTexture = ice;
			break;
		}
}
}
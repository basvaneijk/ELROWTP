using UnityEngine;
using System.Collections;

public class klantRequest : MonoBehaviour {
	GameObject requestWolk;
	Renderer wolkRenderer;
	public Texture sandwich;
	public Texture cake;
	public Texture ice;
public	// Use this for initialization
	void Start () {
		requestWolk = GameObject.Find ("klant_wolkje/klant_request");
		wolkRenderer = requestWolk.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		int a = Random.Range (0, 3);
		switch (a) {
		case 1:
			wolkRenderer.material.mainTexture = sandwich;
			break;
		case 2:
			wolkRenderer.material.mainTexture = cake;
			break;
		case 3:
			wolkRenderer.material.mainTexture = ice;
			break;
		}
	}
}

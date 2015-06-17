using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public WebCamTexture mCamera = null;
	public GameObject plane;
	
	// Use this for initialization
	void Start ()
	{
        Screen.orientation = ScreenOrientation.LandscapeLeft;
		Debug.Log ("Script has been started");
		plane = GameObject.FindWithTag ("Webcamtexture");
		float height = (float) Camera.main.orthographicSize * 2.0f;
		float width = (float) height * Screen.width / Screen.height;
		plane.transform.localScale = new Vector3(width/10,0.1f,height/17);
        mCamera = new WebCamTexture("Logitech HD Webcam C270");
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;
		mCamera.Play();
        foreach (WebCamDevice g in WebCamTexture.devices)
        {
            Debug.Log(g.name);
        }

	}
	
	// Update is called once per frame
	void Update()
	{
       
	}
}

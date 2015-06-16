using UnityEngine;
using System.Collections;

public class RotateCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {


        
	}

    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.back, 1);
     
    }

}

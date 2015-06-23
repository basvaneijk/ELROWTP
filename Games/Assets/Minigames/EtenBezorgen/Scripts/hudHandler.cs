using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hudHandler : MonoBehaviour {

    public RectTransform TipTransform;
    private float minXValue;
    private float maxXValue;
    public Text tipText;
    public GameObject kitchen;
	// Use this for initialization
    void Start()
    {
        maxXValue = TipTransform.position.x;
        minXValue = TipTransform.position.x - TipTransform.rect.width;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (kitchen.GetComponent<tipCounter>().started())
        {
            tipText.text = "Fooi: " + kitchen.GetComponent<tipCounter>().getTip();

            //TipTransform.position = new Vector3((((kitchen.GetComponent<tipCounter>().getMaxtip() - kitchen.GetComponent<tipCounter>().getTip()) * (TipTransform.rect.width / kitchen.GetComponent<tipCounter>().getMaxtip())) * -1), TipTransform.position.y);
        }
    }
}

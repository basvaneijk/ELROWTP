using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hudHandler : MonoBehaviour {

    public RectTransform TipTransform;
    private float minXValue;
    private float maxXValue;
    public Text tipText;
    public GameObject kitchen;
    public GameObject tipHUD;
	// Use this for initialization
    void Start()
    {
        maxXValue = TipTransform.position.x;
        minXValue = TipTransform.position.x - TipTransform.rect.width;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!tipHUD.activeInHierarchy && kitchen.GetComponent<tipCounter>().started())
        {
            
            tipHUD.SetActive(true);
        }
        else if (tipHUD.activeInHierarchy && !kitchen.GetComponent<tipCounter>().started())
        {
            tipHUD.SetActive(false);
        }
        if (kitchen.GetComponent<tipCounter>().started())
        {
            tipText.text = "Fooi: " + kitchen.GetComponent<tipCounter>().getTip();
            
            
            if (kitchen.GetComponent<tipCounter>().getTip() < 0.10F)
            {
                TipTransform.position = new Vector3(minXValue, TipTransform.position.y);
            }
            else if (kitchen.GetComponent<tipCounter>().getMaxtip() != kitchen.GetComponent<tipCounter>().getTip())
            {
                TipTransform.position = new Vector3(maxXValue -
                    /*(
                        (TipTransform.rect.width / (kitchen.GetComponent<tipCounter>().getMaxtip() / 0.20f)) *
                    (kitchen.GetComponent<tipCounter>().getMaxtip() - kitchen.GetComponent<tipCounter>().getTip()))

                    */
                (
                (TipTransform.rect.width / kitchen.GetComponent<tipCounter>().getMaxtip()) * 0.20f) *
                (
                (kitchen.GetComponent<tipCounter>().getMaxtip() / 0.20f) /
                (kitchen.GetComponent<tipCounter>().getMaxtip() / 
                (kitchen.GetComponent<tipCounter>().getMaxtip() - kitchen.GetComponent<tipCounter>().getTip()))), TipTransform.position.y);
            }
            else
            {
                TipTransform.position = new Vector3(maxXValue, TipTransform.position.y);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text[] textfields;
    public GameObject[] boxes;
    public new Camera camera;
    GameMaster GM;
	// Use this for initialization
	void Start () {
        textfields = transform.GetComponentsInChildren<Text>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        boxes = GM.boxes.ToArray();
	}
	
	// Update is called once per frame
	void Update () {
        textfields[0].text = boxes[0].transform.GetComponent<Box>().hp.ToString();
        textfields[0].rectTransform.position = camera.WorldToScreenPoint(boxes[0].transform.localPosition);
        textfields[1].text = boxes[1].transform.GetComponent<Box>().hp.ToString();
        textfields[1].rectTransform.position = camera.WorldToScreenPoint(boxes[1].transform.localPosition);
        textfields[2].text = boxes[2].transform.GetComponent<Box>().hp.ToString();
        textfields[2].rectTransform.position = camera.WorldToScreenPoint(boxes[2].transform.localPosition);
        textfields[5].text = boxes[3].transform.GetComponent<Box>().hp.ToString();
        textfields[5].rectTransform.position = camera.WorldToScreenPoint(boxes[3].transform.localPosition);
        textfields[4].text = boxes[4].transform.GetComponent<Box>().hp.ToString();
        textfields[4].rectTransform.position = camera.WorldToScreenPoint(boxes[4].transform.localPosition);
        textfields[3].text = boxes[5].transform.GetComponent<Box>().hp.ToString();
        textfields[3].rectTransform.position = camera.WorldToScreenPoint(boxes[5].transform.localPosition);

        textfields[6].text = GM.score.ToString();

        textfields[8].text  = GM.slowRemaning.ToString();
        textfields[9].text = GM.wallRemaning.ToString();
        textfields[10].text = GM.multiRemaning.ToString();
        textfields[11].text = GM.neutralRemaning.ToString();


    }
}

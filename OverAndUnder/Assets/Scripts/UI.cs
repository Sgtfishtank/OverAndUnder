using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text[] textfilds;
    public new Camera camera;
    GameMaster GM;
	// Use this for initialization
	void Start () {
        textfilds = transform.GetComponentsInChildren<Text>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        textfilds[0].text = GM.boxes[0].transform.GetComponent<Box>().hp.ToString();
        textfilds[0].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[0].transform.localPosition);
        textfilds[1].text = GM.boxes[1].transform.GetComponent<Box>().hp.ToString();
        textfilds[1].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[1].transform.localPosition);
        textfilds[2].text = GM.boxes[2].transform.GetComponent<Box>().hp.ToString();
        textfilds[2].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[2].transform.localPosition);
        textfilds[5].text = GM.boxes[3].transform.GetComponent<Box>().hp.ToString();
        textfilds[5].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[3].transform.localPosition);
        textfilds[4].text = GM.boxes[4].transform.GetComponent<Box>().hp.ToString();
        textfilds[4].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[4].transform.localPosition);
        textfilds[3].text = GM.boxes[5].transform.GetComponent<Box>().hp.ToString();
        textfilds[3].rectTransform.position = camera.WorldToScreenPoint(GM.boxes[5].transform.localPosition);

        textfilds[6].text = GM.score.ToString();
	
	}
}

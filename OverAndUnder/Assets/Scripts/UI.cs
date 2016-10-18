using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text[] textfields;
    public GameObject[] boxes;
    public new Camera camera;
    public GameObject GameOver;
    public GameObject MainMenuButton;
    public GameObject startMenu;
    GameMaster GM;
	// Use this for initialization
	void Start () {
        textfields = transform.GetComponentsInChildren<Text>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        boxes = GM.boxes.ToArray();
        GameOver.SetActive(false);
        MainMenuButton.SetActive(false);
	}
    void Awake()
    {
        //GameOver.SetActive(false);
        MainMenuButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(boxes.Length == 0)
        {
            boxes = GM.boxes.ToArray();
            return;
        }
        textfields[0].text = checkZero(boxes[0].transform.GetComponent<Box>().hp);
        textfields[0].rectTransform.position = camera.WorldToScreenPoint(boxes[0].transform.localPosition);
        textfields[1].text = checkZero(boxes[1].transform.GetComponent<Box>().hp);
        textfields[1].rectTransform.position = camera.WorldToScreenPoint(boxes[1].transform.localPosition);
        textfields[2].text = checkZero(boxes[2].transform.GetComponent<Box>().hp);
        textfields[2].rectTransform.position = camera.WorldToScreenPoint(boxes[2].transform.localPosition);
        textfields[5].text = checkZero(boxes[3].transform.GetComponent<Box>().hp);
        textfields[5].rectTransform.position = camera.WorldToScreenPoint(boxes[3].transform.localPosition);
        textfields[4].text = checkZero(boxes[4].transform.GetComponent<Box>().hp);
        textfields[4].rectTransform.position = camera.WorldToScreenPoint(boxes[4].transform.localPosition);
        textfields[3].text = checkZero(boxes[5].transform.GetComponent<Box>().hp);
        textfields[3].rectTransform.position = camera.WorldToScreenPoint(boxes[5].transform.localPosition);

        textfields[6].text = GM.blueScore.ToString();
        textfields[7].text = GM.redScore.ToString();

        textfields[8].text = Mathf.Clamp((GM.slowRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity).ToString();

        textfields[9].text = Mathf.Clamp((GM.wallRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity).ToString();

        textfields[10].text = Mathf.Clamp((GM.multiRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity).ToString();

        textfields[11].text = Mathf.Clamp((GM.switchRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity).ToString();

        if(GM.GameOver)
        {
            for (int i = 0; i < 11; i++)
            {
                textfields[i].gameObject.SetActive(false);
            }
            GameOver.SetActive(true);
            MainMenuButton.SetActive(true);
            
            textfields[12].text = GM.blueScore.ToString();
            textfields[13].text = GM.redScore.ToString();
        }

    }
    public void mainMenu()
    {
        startMenu.SetActive(true);
        //gameObject.SetActive(false);
    }
    string checkZero(int value)
    {
        if (value <= 0)
            return "";
        else
            return value.ToString();
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopLevelselct : MonoBehaviour {
    public Text[] texts;
    internal int currentLevel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangeText(int level)
    {
        currentLevel = level;
        texts[0].text = ConfigReader.Instance.getValueInt("HighScoreLevel" + currentLevel).ToString();
        int starReq = ConfigReader.Instance.getValueInt("StarRequirementLevel" + currentLevel);
        texts[1].text = starReq.ToString();
        texts[2].text = (starReq*2).ToString();
        texts[3].text = (starReq*3).ToString();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Text[] textfields;
    public GameObject[] boxes;
    public GameObject[] scoreMeterStars;
    public GameObject[] tutorialText;
    public new Camera camera;
    public GameObject GameOver;
    public GameObject gameoverMesh;
    public GameObject MainMenuButton;
    public GameObject startMenu;

    GameMaster GM;
    Abilitys AM;
    public GameObject ContinueGameButton;
    public GameObject Settings;
    public GameObject TutorialButton;
    public GameObject Tutorial1;
    public GameObject NextTutButton;
    private int Tut;
    private int totalScore;
    public GameObject Tutorial2;
    public GameObject SettingsButton;
    public GameObject scoreMeterBlue;
    public GameObject[] GameOverStars;
    public GameObject blueParitcle;
    public GameObject redParitcle;
    private int blueScoreBegin;
    private int redScoreBegin;
    private int blueScoreEnd;
    private int redScoreEnd;
    public int stars;
    private int currentLevel;

    private float scalefactor = (1.175139f - 0.01587644f) / 300;
    private float posfactor = (0.055496f - 0.05536f) / 300;
    bool first;
    float lastTick;
    public float scale = 0.05f;

    // Use this for initialization
    void Start () {
        //textfields = transform.GetComponentsInChildren<Text>();
        GM = GameObject.Find("Game Master(Clone)").GetComponent<GameMaster>();
        AM = GameObject.Find("Game Master(Clone)").GetComponent<Abilitys>();
        boxes = GM.boxes.ToArray();
        gameoverMesh = Instantiate(gameoverMesh, Vector3.zero, Quaternion.identity) as GameObject;
        gameoverMesh.SetActive(true);
        scoreMeterBlue = GameObject.FindGameObjectWithTag("Finish");
        redParitcle = GameObject.Find("particles_gameover_crystalsred");
        blueParitcle = GameObject.Find("particles_gameover_crystalsblue");
        GameOver.SetActive(false);
        gameoverMesh.SetActive(false);
        MainMenuButton.SetActive(false);
        Settings.SetActive(false);
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        TutorialButton.SetActive(false);
        NextTutButton.SetActive(false);
        ContinueGameButton.SetActive(false);
        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].SetActive(false);
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
        first = false;
        redParitcle.SetActive(true);
        blueParitcle.SetActive(true);
        redScoreEnd = 0;
        redScoreBegin = 0;
        blueScoreBegin = 0;
        blueScoreEnd = 0;
    }
    public void Reset(int level)
    {
        currentLevel = level;
        GameOver.SetActive(true);
        gameoverMesh.SetActive(true);
        GM = GameObject.Find("Game Master(Clone)").GetComponent<GameMaster>();
        if(textfields.Length == 0)
            textfields = transform.GetComponentsInChildren<Text>();
        boxes = GM.boxes.ToArray();
        for (int i = 0; i < 7; i++)
        {
            textfields[i].gameObject.SetActive(true);
        }
        GameOver.SetActive(false);
        gameoverMesh.SetActive(false);
        MainMenuButton.SetActive(false);
        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].SetActive(false);
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
        first = false;
        redScoreBegin = 0;
        blueScoreBegin = 0;
        redScoreEnd = 0;
        blueScoreEnd = 0;
        if(currentLevel == 1)
        {
            tutorialText[0].SetActive(true);
            tutorialText[1].SetActive(false);
            tutorialText[2].SetActive(false);
        }
        else if (currentLevel == 2)
        {
            tutorialText[0].SetActive(false);
            tutorialText[1].SetActive(true);
            tutorialText[2].SetActive(false);
        }
        else if (currentLevel == 3)
        {
            tutorialText[0].SetActive(false);
            tutorialText[1].SetActive(false);
            tutorialText[2].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(boxes.Length == 0)
        {
            
            boxes = GM.boxes.ToArray();
            return;
        }
        
        textfields[2].text = checkZero(boxes[2].transform.GetComponent<Box>().hp);
        textfields[2].rectTransform.position = camera.WorldToScreenPoint(boxes[2].transform.localPosition) + new Vector3(0, 7, 0);
        textfields[5].text = checkZero(boxes[5].transform.GetComponent<Box>().hp);
        textfields[5].rectTransform.position = camera.WorldToScreenPoint(boxes[5].transform.localPosition) + new Vector3(0, 7, 0);
        if (currentLevel > 3)
        {
            textfields[1].text = checkZero(boxes[1].transform.GetComponent<Box>().hp);
            textfields[1].rectTransform.position = camera.WorldToScreenPoint(boxes[1].transform.localPosition) + new Vector3(0, 7, 0);
            textfields[4].text = checkZero(boxes[4].transform.GetComponent<Box>().hp);
            textfields[4].rectTransform.position = camera.WorldToScreenPoint(boxes[4].transform.localPosition) + new Vector3(0, 7, 0);
        }
        else if (currentLevel > 9)
        {
            textfields[0].text = checkZero(boxes[0].transform.GetComponent<Box>().hp);
            textfields[0].rectTransform.position = camera.WorldToScreenPoint(boxes[0].transform.localPosition) + new Vector3(0, 7, 0);
            textfields[3].text = checkZero(boxes[3].transform.GetComponent<Box>().hp);
            textfields[3].rectTransform.position = camera.WorldToScreenPoint(boxes[3].transform.localPosition) + new Vector3(0, 7, 0);
        }
        totalScore = (GM.redScore + GM.blueScore);
        textfields[6].text = totalScore.ToString();

        /*textfields[7].text = GM.blueScore.ToString();
        textfields[8].text = GM.redScore.ToString();*/
        int temp = (int)Mathf.Clamp((AM.slowRemaning - Mathf.FloorToInt(Time.time)), 0, Mathf.Infinity);

            textfields[7].text = checkZero(temp);

        if(GM.GameOver)
        {
            tutorialText[0].SetActive(false);
            tutorialText[1].SetActive(false);
            tutorialText[2].SetActive(false);
            for (int i = 0; i < 7; i++)
            {
                textfields[i].gameObject.SetActive(false);
            }
            GameOver.SetActive(true);
            gameoverMesh.SetActive(true);
            MainMenuButton.SetActive(true);
            for (int i = 0; i < GameOverStars.Length; i++)
            {
                GameOverStars[i].transform.parent.gameObject.SetActive(true);
            }
            
            textfields[8].text = blueScoreEnd.ToString();
            textfields[9].text = redScoreEnd.ToString();
            int totalScoreCount = blueScoreBegin + redScoreBegin;
            textfields[10].text = totalScoreCount.ToString();
            if (totalScoreCount > 299 && !GameOverStars[2].activeSelf)
            {
                GameOverStars[2].SetActive(true);
                stars++;
            }
            if (totalScoreCount > 199 && !GameOverStars[1].activeSelf)
            {
                GameOverStars[1].SetActive(true);
                stars++;
            }
            if (totalScoreCount > 99 && !GameOverStars[0].activeSelf)
            {
                GameOverStars[0].SetActive(true);
                stars++;
            }
            if(!first)
            {
                blueScoreEnd = GM.blueScore;
                redScoreEnd = GM.redScore;
                first = true;
            }
            if(lastTick < Time.time)
            {
                scoreMover();
                lastTick = Time.time + scale;
            }
            if(blueScoreBegin == blueScoreEnd)
            {
                blueParitcle.SetActive(false);
            }
            if(redScoreBegin == redScoreEnd)
            {
                redParitcle.SetActive(false);
            }
        }
    }
    void scoreMover()
    {
        if (blueScoreBegin != blueScoreEnd)
        {
        blueScoreBegin++;
        
        }
        if (redScoreBegin != redScoreEnd)
        {
            redScoreBegin++;
        }
        if (blueScoreBegin < 301)
        {
            scoreMeterBlue.transform.localPosition = new Vector3(scoreMeterBlue.transform.localPosition.x, 0.055496f + (posfactor * (redScoreBegin + blueScoreBegin)), scoreMeterBlue.transform.localPosition.z);
            scoreMeterBlue.transform.localScale = new Vector3(scoreMeterBlue.transform.localScale.x, 0.01587644f + (scalefactor * (redScoreBegin + blueScoreBegin)), scoreMeterBlue.transform.localScale.z);
        }
        else
        {
            scoreMeterBlue.transform.localPosition = new Vector3(scoreMeterBlue.transform.localPosition.x, 0.055496f + (posfactor * 300), scoreMeterBlue.transform.localPosition.z);
            scoreMeterBlue.transform.localScale = new Vector3(scoreMeterBlue.transform.localScale.x, 0.01587644f + (scalefactor * 300), scoreMeterBlue.transform.localScale.z);
        }
        if (GM.blueScore > 99 && GM.redScore > 99)
            scoreMeterStars[0].SetActive(true);
        if (GM.blueScore > 199 && GM.redScore > 199)
            scoreMeterStars[1].SetActive(true);
        if (GM.blueScore > 299 && GM.redScore > 299)
            scoreMeterStars[2].SetActive(true);
    }
    public void mainMenu()
    {
        startMenu.SetActive(true);
        startMenu.GetComponent<MainMenu>().reset();
        gameObject.SetActive(false);
        gameoverMesh.SetActive(false);
        GM.gameObject.SetActive(false);
        for(int i = 0; i < 6;i++)
        {
            GM.boxes[i].SetActive(false);
        }

        for (int i = 0; i < GameOverStars.Length; i++)
        {
            GameOverStars[i].transform.parent.gameObject.SetActive(false);
        }
    }
    public void SettingsFunc()
    {
        Time.timeScale = 0;
        Settings.SetActive(true);
        SettingsButton.SetActive(true);
        ContinueGameButton.SetActive(true);
        TutorialButton.SetActive(true);
        for (int i = 0; i < textfields.Length; i++)
        {
            textfields[i].gameObject.SetActive(false);
        }

    }
    public void TutorialFunc()
    {
        Settings.SetActive(false);
        TutorialButton.SetActive(false);
        Tutorial1.SetActive(true);
        NextTutButton.SetActive(true);
        Tut = 1;
    }
    public void TutorialNextFunc()
    {
        if (Tut == 1)
        {
            Tutorial1.SetActive(false);
            Tutorial2.SetActive(true);
            Tut++;
        }
        else
        {
            Tutorial2.SetActive(false);
            NextTutButton.SetActive(false);
            Settings.SetActive(true);
            ContinueGameButton.SetActive(true);
        }
    }
    public void Continue()
    {
        Settings.SetActive(false);
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        TutorialButton.SetActive(false);
        NextTutButton.SetActive(false);
        ContinueGameButton.SetActive(false);
        for (int i = 0; i < textfields.Length; i++)
        {
            textfields[i].gameObject.SetActive(true);
        }
        Time.timeScale = 1;
    }
    string checkZero(int value)
    {
        if (value <= 0)
            return "";
        else
            return value.ToString();
    }

}

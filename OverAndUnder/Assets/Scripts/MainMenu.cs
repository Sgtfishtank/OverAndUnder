using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class MainMenu : MonoBehaviour
{
    public GameObject StartGameButton;
    public GameObject StartScreen;
    public GameObject SettingsButton;
    public GameObject SettingsButton1;
    public GameObject TutorialButton;
    public GameObject NextTutButton;
    public GameObject MainMenuButton;
    public GameObject UpgradeButton;
    public GameObject LevelButtons;
    public GameObject LevelSelect;
    public GameObject Settings;
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject GM;
    public GameObject InGameUI;
    public GameObject Upgrades;
    public GameObject UpgradeSkillButtons;
    public GameObject[] levelStars;
    public GameObject[] forwardArrows;
    public GameObject[] levelRomb;
    public Material matArrows1;
    public Material matArrows2;
    public Material matBackground1;
    public Material matBackground2;
    public GameObject[] locks;
    public GameObject credits;
    public GameObject lights;
    public GameObject lightsIngame;
    int Tut = 1;
    public int bluescore;
    public int redscore;
    private int LevelPlayed;


    // Use this for initialization
    void Start ()
    {
        GM = Instantiate(GM, new Vector3(-2.15f,0,0), Quaternion.identity) as GameObject;
        GM.SetActive(false);
        InGameUI.SetActive(false);
        Upgrades = Instantiate(Upgrades, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
        LevelSelect = Instantiate(LevelSelect,Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
        StartScreen = Instantiate(StartScreen, new Vector3(0.1134949f,0,0), Quaternion.Euler(0,180,0)) as GameObject;
        Settings = Instantiate(Settings, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
        levelStars = GameObject.FindGameObjectsWithTag("LevelStars").OrderBy(go => go.name).ToArray();
        forwardArrows = GameObject.FindGameObjectsWithTag("ForwardArrows").OrderBy(go => go.name).ToArray();
        locks = GameObject.FindGameObjectsWithTag("Locks").OrderBy(go => go.name).ToArray();
        levelRomb = GameObject.FindGameObjectsWithTag("MenuBackButton").OrderBy(go => go.name).ToArray();
        credits = GameObject.FindGameObjectWithTag("Credits");
        lights = GameObject.Find("Point light_menus");
        lightsIngame = GameObject.Find("Point light");
        lightsIngame.SetActive(false);
        Upgrades.SetActive(false);
        LevelSelect.SetActive(false);
        StartScreen.SetActive(true);
        credits.SetActive(false);
        Settings.SetActive(false);
        SettingsButton1.SetActive(false);
    }
    public void reset()
    {
        InGameUI.SetActive(false);
        LevelSelect.SetActive(false);
        LevelButtons.SetActive(false);
        StartScreen.SetActive(true);
        StartGameButton.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void LevelSelectFunc()
    {
        StartScreen.SetActive(false);
        StartGameButton.SetActive(false);
        LevelButtons.SetActive(true);
        LevelSelect.SetActive(true);
        MainMenuButton.SetActive(true);
        Button[] buttons = LevelButtons.GetComponentsInChildren<Button>();
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        levelSelectButtons();
    }
    public void StartGameFunc(int level)
    {
        GM.SetActive(true);
        GM.GetComponent<GameMaster>().Reset(level);
        GM.GetComponent<Abilitys>().Reset(level);
        InGameUI.SetActive(true);
        InGameUI.GetComponent<UI>().Reset(level);
        StartScreen.SetActive(false);
        gameObject.SetActive(false);
        LevelSelect.SetActive(false);
        LevelPlayed = level;
        lights.SetActive(false);
        lightsIngame.SetActive(true);
    }
    public void SettingsFunc()
    {
        StartScreen.SetActive(false);
        StartGameButton.SetActive(false);
        SettingsButton.SetActive(false);
        Settings.SetActive(true);
        TutorialButton.SetActive(true);
        MainMenuButton.SetActive(true);
        UpgradeButton.SetActive(false);
        SettingsButton1.SetActive(true);

    }
    public void UppgradeFunc()
    {
        StartGameButton.SetActive(false);
        StartScreen.SetActive(false);
        UpgradeButton.SetActive(false);
        Upgrades.SetActive(true);
        UpgradeSkillButtons.SetActive(true);
        MainMenuButton.SetActive(true);
    }
    public void MainMenuFunc()
    {
        StartScreen.SetActive(true);
        StartGameButton.SetActive(true);
        SettingsButton.SetActive(true);
        Settings.SetActive(false);
        MainMenuButton.SetActive(false);
        Upgrades.SetActive(false);
        UpgradeSkillButtons.SetActive(false);
        LevelSelect.SetActive(false);
        LevelButtons.SetActive(false);
        SettingsButton1.SetActive(false);
    }
    public void UpgradeSlowBlue(int nr)
    {
        if(nr  == 1 && bluescore > 100)
        {
            print("blue 1");
            bluescore -= 100;
        }
        else if(nr == 2 && bluescore > 200)
        {
            print("blue 2");
            bluescore -= 200;
        }
        else if (nr == 3 && bluescore > 300)
        {
            print("blue 3");
            bluescore -= 300;
        }
    }
    public void UpgradeSlowRed(int nr)
    {
        if (nr == 1 && redscore > 100)
        {
            print("Red 1");
            redscore -= 100;
        }
        else if (nr == 2 && redscore > 200)
        {
            print("red 2");
            redscore -= 100;
        }
        else if (nr == 3 && redscore > 300)
        {
            print("red 3");
            redscore -= 300;
        }
    }
    public void UpgradeShiled(int nr)
    {}
    public void levelSelectButtons()
    {
        int totalstars = 0;
        for (int i = 1; i < 16; i++)
        {
            totalstars += ConfigReader.Instance.getValue("StarsLevel" + (i));
        }
        if (totalstars > 5)
        {
            locks[0].GetComponent<MeshRenderer>().sharedMaterial = matBackground1;
            locks[2].SetActive(false);
        }
        if (totalstars > 17)
        {
            locks[1].GetComponent<MeshRenderer>().sharedMaterial = matBackground1;
            locks[3].SetActive(false);
        }
        Text[] text = LevelButtons.GetComponentsInChildren<Text>(true);
        text[0].text = Mathf.Min(totalstars, 6).ToString();
        text[3].text = Mathf.Min(totalstars, 18).ToString();


        Button[] buttons = LevelButtons.GetComponentsInChildren<Button>(true);

        for (int i = 1; i < buttons.Length; i++)
        {
            if(ConfigReader.Instance.getValue("StarsLevel"+(i)) >0)
            {
                if (i == 3 && totalstars < 6)
                {}
                else if (i == 9 && totalstars < 18)
                {}
                else
                    buttons[i].gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < levelStars.Length; i++)
        {
            int stars = ConfigReader.Instance.getValue("StarsLevel" + (i + 1));
            if (stars > 0)
            {
                GameObject[] temp = levelStars[i].GetComponentsInChildren<Transform>(true).Where(x=>x.name == "mesh_levelselect_starcore").Select(x => x.transform.gameObject).ToArray();
                if(stars >= 1)
                {
                    temp[0].SetActive(true);
                }
                if (stars >= 2)
                {
                    temp[1].SetActive(true);
                }
                if (stars == 3)
                {
                    temp[2].SetActive(true);
                }

            }
        }
        int increes = 1;
        for(int i = 0; i < 12; i++)
        {
            if(i == 2)
                increes++;
            int stars = ConfigReader.Instance.getValue("StarsLevel" + (i + increes));
            if(stars > 0)
            {
                forwardArrows[i].GetComponent<MeshRenderer>().sharedMaterial = matArrows1;
            }
            else
            {
                forwardArrows[i].GetComponent<MeshRenderer>().sharedMaterial = matArrows2;
            }

        }
        for(int i = 1; i < 15; i++)
        {
            int stars = ConfigReader.Instance.getValue("StarsLevel" + (i));
            if (stars > 0)
            {
                levelRomb[i].GetComponent<MeshRenderer>().sharedMaterial = matArrows1;
            }
            else
            {
                levelRomb[i].GetComponent<MeshRenderer>().sharedMaterial = matBackground2;
            }
            if ((i == 3 && totalstars < 6) || (i == 9 && totalstars < 18))
            {
                levelRomb[i].GetComponent<MeshRenderer>().sharedMaterial = matBackground2;
            }
        }
    }
}
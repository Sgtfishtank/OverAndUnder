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
    public GameObject MainMenuButton;
    public GameObject UpgradeButton;
    public GameObject LevelButtons;
    public GameObject LevelSelect;
    public GameObject GM;
    public GameObject InGameUI;
    public GameObject Upgrades;
    public GameObject UpgradeSkillButtons;
    public GameObject UpgradeCanvas;
    public GameObject PopUp;
    public GameObject PopUpCanvas;
    public GameObject[] levelStars;
    public GameObject[] forwardArrows;
    public GameObject[] levelRomb;
    public GameObject[] helpScreen;
    public Material matBackground1;
    public Material matBackground2;
    public Material matRomb;
    public GameObject[] locks;
    public GameObject lights;
    public GameObject lightsIngame;
    public string[] temp;
    public GameObject[] upgradeLock;
    
    int Tut = 1;
    public int score;
    private int LevelPlayed;
    


    // Use this for initialization
    void Start ()
    {
        GM = Instantiate(GM, new Vector3(-2.15f,0,0), Quaternion.identity) as GameObject;
        GM.SetActive(false);
        InGameUI.SetActive(false);
        Upgrades = Instantiate(Upgrades, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
        PopUp = Instantiate(PopUp, new Vector3(0,0,-1), Quaternion.Euler(0, 180, 0)) as GameObject;
        LevelSelect = Instantiate(LevelSelect,Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
        StartScreen = Instantiate(StartScreen, new Vector3(0.1134949f,0,0), Quaternion.Euler(0,180,0)) as GameObject;
        helpScreen[0] = Instantiate(helpScreen[0], new Vector3(0, 0, -1), Quaternion.Euler(0, 180, 0)) as GameObject;
        levelStars = GameObject.FindGameObjectsWithTag("LevelStars").OrderBy(go => go.name).ToArray();
        forwardArrows = GameObject.FindGameObjectsWithTag("ForwardArrows").OrderBy(go => go.name).ToArray();
        locks = GameObject.FindGameObjectsWithTag("Locks").OrderBy(go => go.name).ToArray();
        levelRomb = GameObject.FindGameObjectsWithTag("MenuBackButton").OrderBy(go => go.name).ToArray();
        lights = GameObject.Find("Point light_menus");
        lightsIngame = GameObject.Find("Point light");
        lightsIngame.SetActive(false);
        UpgradeCanvas.SetActive(false);
        Upgrades.SetActive(false);
        LevelSelect.SetActive(false);
        StartScreen.SetActive(true);
        helpScreen[0].SetActive(false);
        PopUp.SetActive(false);
        temp = ConfigReader.Instance.lines;
        upgradeLock[0] = GameObject.FindGameObjectWithTag("UpgradeTextSC");
        upgradeLock[1] = GameObject.FindGameObjectWithTag("UpgradeBottonMatChange");
        upgradeLock[2] = GameObject.FindGameObjectWithTag("UpgradeLock");
        score = ConfigReader.Instance.getValue("CrystalsBanked");
        upgradeLock[0].transform.GetComponent<Text>().text = (ConfigReader.Instance.getValue("StarsLevel1") + ConfigReader.Instance.getValue("StarsLevel2") + ConfigReader.Instance.getValue("StarsLevel3")).ToString();
        if (ConfigReader.Instance.getValue("StarsLevel1") + ConfigReader.Instance.getValue("StarsLevel2") + ConfigReader.Instance.getValue("StarsLevel3") > 5)
        {
            upgradeLock[1].transform.GetComponent<MeshRenderer>().sharedMaterial = matBackground1;
            upgradeLock[2].SetActive(false);
            upgradeLock[3].SetActive(true);
        }
    }
    public void reset()
    {
        InGameUI.SetActive(false);
        LevelSelect.SetActive(false);
        LevelButtons.SetActive(false);
        StartScreen.SetActive(true);
        StartGameButton.SetActive(true);
        GM.SetActive(false);
        score = ConfigReader.Instance.getValue("CrystalsBanked");
        upgradeLock[0].transform.GetComponent<Text>().text = (ConfigReader.Instance.getValue("StarsLevel1") + ConfigReader.Instance.getValue("StarsLevel2") + ConfigReader.Instance.getValue("StarsLevel3")).ToString();
        if (ConfigReader.Instance.getValue("StarsLevel1")+ ConfigReader.Instance.getValue("StarsLevel2")+ ConfigReader.Instance.getValue("StarsLevel3") > 5)
        {
            upgradeLock[1].transform.GetComponent<MeshRenderer>().sharedMaterial = matBackground1;
            upgradeLock[2].SetActive(false);
            upgradeLock[3].SetActive(true);
        }
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
        transform.parent.GetComponentInChildren<UniversalCanvas>().toggle(false);
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        helpScreen[1].SetActive(true);
        levelSelectButtons();
        upgradeLock[4].SetActive(false);
    }
    public void PopUpFunc(int level)
    {
        PopUp.SetActive(true);
        PopUpCanvas.SetActive(true);
        LevelButtons.SetActive(false);
        PopUpCanvas.transform.GetComponent<PopLevelselct>().ChangeText(level);
        LevelPlayed = level;
    }
    public void StartGameFunc()
    {
        GM.SetActive(true);
        PopUp.SetActive(false);
        PopUpCanvas.SetActive(false);
        GM.GetComponent<GameMaster>().Reset(LevelPlayed);
        GM.GetComponent<Abilitys>().Reset(LevelPlayed);
        InGameUI.SetActive(true);
        InGameUI.GetComponent<UI>().Reset(LevelPlayed);
        InGameUI.GetComponent<UI>().CountDown();
        transform.parent.GetComponentInChildren<UniversalCanvas>().changeState();
        transform.parent.GetComponentInChildren<UniversalCanvas>().toggle(true);
        StartScreen.SetActive(false);
        gameObject.SetActive(false);
        lights.SetActive(false);
        lightsIngame.SetActive(true);
        LevelSelect.SetActive(false);
        helpScreen[1].SetActive(false);
        ConfigReader.Instance.changeValue("GamesPlayed", ConfigReader.Instance.getValue("GamesPlayed")+1);
    }
    public void UppgradeFunc()
    {
        StartGameButton.SetActive(false);
        StartScreen.SetActive(false);
        UpgradeButton.SetActive(false);
        Upgrades.SetActive(true);
        UpgradeSkillButtons.SetActive(true);
        MainMenuButton.SetActive(true);
        UpgradeCanvas.SetActive(true);
        transform.parent.GetComponentInChildren<UniversalCanvas>().toggle(false);
        UpgradeCanvas.GetComponent<UpgradeScript>().selectButtons();
        helpScreen[3].SetActive(true);
        upgradeLock[4].SetActive(false);
    }
    public void MainMenuFunc()
    {
        StartScreen.SetActive(true);
        StartGameButton.SetActive(true);
        SettingsButton.SetActive(true);
        MainMenuButton.SetActive(false);
        Upgrades.SetActive(false);
        UpgradeButton.SetActive(true);
        UpgradeSkillButtons.SetActive(false);
        UpgradeCanvas.SetActive(false);
        transform.parent.GetComponentInChildren<UniversalCanvas>().toggle(true);
        LevelSelect.SetActive(false);
        LevelButtons.SetActive(false);
        helpScreen[1].SetActive(false);
        helpScreen[3].SetActive(false);
        upgradeLock[4].SetActive(true);
    }
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
            if(i == 2 || i == 7)
                increes++;
            int stars = ConfigReader.Instance.getValue("StarsLevel" + (i + increes));
            if(stars > 0)
            {
                forwardArrows[i].gameObject.SetActive(true);
            }
            else
            {
                forwardArrows[i].gameObject.SetActive(false);
            }

        }
        for(int i = 1; i < 15; i++)
        {
            int stars = ConfigReader.Instance.getValue("StarsLevel" + (i));
            if (stars > 0)
            {
                levelRomb[i].GetComponent<MeshRenderer>().sharedMaterial = matRomb;
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
    public void helpLevelSelct()
    {
        helpScreen[0].SetActive(true);
        helpScreen[2].SetActive(true);
        helpScreen[5].SetActive(true);
    }
    public void helpUpgrade()
    {
        helpScreen[0].SetActive(true);
        helpScreen[4].SetActive(true);
        helpScreen[5].SetActive(true);
    }
    public void back()
    {
        helpScreen[0].SetActive(false);
        helpScreen[2].SetActive(false);
        helpScreen[4].SetActive(false);
        helpScreen[5].SetActive(false);
    }
    public void BackPopUp()
    {
        PopUp.SetActive(false);
        PopUpCanvas.SetActive(false);
        LevelButtons.SetActive(true);
    }
}
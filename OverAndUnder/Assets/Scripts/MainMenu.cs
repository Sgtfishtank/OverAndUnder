using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject StartGameButton;
    public GameObject StartScreen;
    public GameObject SettingsButton;
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
        StartScreen = Instantiate(StartScreen, Vector3.zero, Quaternion.Euler(0,180,0)) as GameObject;
        Upgrades.SetActive(false);
        LevelSelect.SetActive(false);
        StartScreen.SetActive(true);
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
    }
    public void SettingsFunc()
    {
        StartScreen.SetActive(false);
        StartGameButton.SetActive(false);
        SettingsButton.SetActive(false);
        Settings.SetActive(true);
        TutorialButton.SetActive(true);
        MainMenuButton.SetActive(true);

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
        Button[] buttons = LevelButtons.GetComponentsInChildren<Button>();
        for (int i = 1; i < buttons.Length; i++)
        {
            if(ConfigReader.Instance.getValue("StarsLevel"+(i+1)) >0)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
}

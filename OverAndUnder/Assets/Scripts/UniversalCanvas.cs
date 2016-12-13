using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UniversalCanvas : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject ToggleSettingsButton;
    public GameObject SettingsButton1;
    public GameObject MainMenuObj;
    public GameObject SettingObj;
    public GameObject MainMenuButton;
    public GameObject MainMenuButton2;
    public GameObject CreditsObj;
    public GameObject StatisticsObj;
    public GameObject StatisticsCanvas;
    public GameObject BackButton;
    public GameObject BackButton2;
    public GameObject AreUSure;
    public GameObject InGameCanvas;
    private GameObject mainMenuGraphics;
    private Scrollbar[] Scrollbars;
    bool inGame = false;
    
    // Use this for initialization
    void Start ()
    {
        SettingObj = Instantiate(SettingObj, new Vector3(0, 0, -1), Quaternion.Euler(0, 180, 0)) as GameObject;
        StatisticsObj = Instantiate(StatisticsObj, new Vector3(0, 0, -1), Quaternion.Euler(0, 180, 0)) as GameObject;
        CreditsObj = GameObject.FindGameObjectWithTag("Credits");
        Scrollbars = SettingsButton1.GetComponentsInChildren<Scrollbar>();
        mainMenuGraphics = GameObject.FindGameObjectWithTag("MainMenuGraphics");
        mainMenuGraphics.SetActive(false);
        StatisticsObj.SetActive(false);
        CreditsObj.SetActive(false);
        StatisticsCanvas.SetActive(false);
        SettingsButton1.SetActive(false);
        SettingObj.SetActive(false);
        BackButton.SetActive(false);
        AreUSure.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    public void Stats()
    {
        StatisticsCanvas.SetActive(true);
        StatisticsObj.SetActive(true);
        SettingObj.SetActive(false);
        BackButton.SetActive(true);
        MainMenuButton.SetActive(false);
        SettingsButton1.SetActive(false);
        BackButton2.SetActive(false);
        statisticWriter();
    }
    public void Back()
    {
        CreditsObj.SetActive(false);
        SettingObj.SetActive(true);
        StatisticsCanvas.SetActive(false);
        StatisticsObj.SetActive(false);
        BackButton2.SetActive(true);
        if (!inGame)
        {
            BackButton.SetActive(false);
            BackButton2.SetActive(false);
        }
        MainMenuButton.SetActive(true);
        SettingsButton1.SetActive(true);
    }
    public void Back2()
    {
        Time.timeScale = 1;
        SettingObj.SetActive(false);
        SettingsButton1.SetActive(false);
        InGameCanvas.SetActive(true);
    }
    public void Credits()
    {
        CreditsObj.SetActive(true);
        SettingsButton1.SetActive(false);
        BackButton.SetActive(true);
        BackButton2.SetActive(false);
        MainMenuButton.SetActive(false);
    }
    public void SoundOff(bool b)
    {

    }
    public void MusicOff(bool b)
    {

    }
    public void SoundLevel()
    {
        //Scrollbars[0].value;
    }
    public void MusicLevel()
    {
        //Scrollbars[1].value;
    }
    public void SettingsFunc()
    {
        CreditsObj.SetActive(false);
        MainMenu.SetActive(false);
        SettingObj.SetActive(true);
        MainMenuButton.SetActive(true);
        SettingsButton1.SetActive(true);
        InGameCanvas.SetActive(false);
        BackButton2.SetActive(false);
        if (inGame)
        {
            Time.timeScale = 0;
            BackButton2.SetActive(true);
            BackButton.SetActive(false);
            MainMenuButton.SetActive(false);
        }
        else
        {

        }
    }
    public void MainMenuFunc()
    {
        if(inGame)
        {
            AreUSure.SetActive(true);
            mainMenuGraphics.SetActive(true);
        }
        else
        {
            CreditsObj.SetActive(false);
            MainMenu.SetActive(true);
            MainMenuButton.SetActive(false);
            SettingsButton1.SetActive(false);
            SettingObj.SetActive(false);
        }
    }
    public void Yes()
    {
        CreditsObj.SetActive(false);
        MainMenu.SetActive(true);
        MainMenuButton.SetActive(false);
        SettingsButton1.SetActive(false);
        SettingObj.SetActive(false);
        MainMenu.GetComponent<MainMenu>().reset();
        AreUSure.SetActive(false);
        if (inGame)
            Time.timeScale = 1;
    }
    public void No()
    {
        AreUSure.SetActive(false);
    }
    public void changeState()
    {
        inGame = !inGame;
    }
    void statisticWriter()
    {
        Text[] temp =  StatisticsCanvas.GetComponentsInChildren<Text>(true);
        int totalStars = 0;
        int levels = 0;
        for (int i = 1; i < 16; i++)
        {
            if (ConfigReader.Instance.getValue("StarsLevel" + i) > 0)
                levels++;
            totalStars += ConfigReader.Instance.getValue("StarsLevel" + i);

        }
        if(levels == 9 && totalStars > 17)
        {
            levels = 10;
        }
        if (levels == 3 && totalStars > 5)
        {
            levels = 4;
        }
        int upgrades = ConfigReader.Instance.getValue("UpgradeHPLevel") + ConfigReader.Instance.getValue("UpgradeDurationLevel") + ConfigReader.Instance.getValue("UpgradeCDLevel");

        temp[1].text = totalStars.ToString();
        temp[5].text = levels.ToString();
        temp[9].text = upgrades.ToString();//uppgrades
        temp[13].text = (totalStars + levels + upgrades).ToString();
        temp[17].text = ConfigReader.Instance.getValue("CrystalsTop").ToString();
        temp[19].text = ConfigReader.Instance.getValue("CrystalsBanked").ToString();
        temp[21].text = ConfigReader.Instance.getValue("CrystalsTotal").ToString();
        temp[23].text = ConfigReader.Instance.getValue("GamesPlayed").ToString();
        temp[25].text = ConfigReader.Instance.getValue("Healed").ToString();
        temp[27].text = ConfigReader.Instance.getValue("SlowUsed").ToString();
        temp[29].text = ConfigReader.Instance.getValue("ShieldLost").ToString();
        temp[31].text = ConfigReader.Instance.getValue("HeartHits").ToString();
    }
    internal void toggle(bool b)
    {
        ToggleSettingsButton.SetActive(b);
    }
}

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
    public GameObject CreditsObj;
    public GameObject StatisticsObj;
    public GameObject StatisticsCanvas;
    public GameObject BackButton;
    private Scrollbar[] Scrollbars;
    
    // Use this for initialization
    void Start ()
    {
        SettingObj = Instantiate(SettingObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
        StatisticsObj = Instantiate(StatisticsObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
        CreditsObj = GameObject.FindGameObjectWithTag("Credits");
        Scrollbars = SettingsButton1.GetComponentsInChildren<Scrollbar>();
        StatisticsObj.SetActive(false);
        CreditsObj.SetActive(false);
        StatisticsCanvas.SetActive(false);
        SettingsButton1.SetActive(false);
        SettingObj.SetActive(false);

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
    }
    public void Back()
    {

    }
    public void Credits()
    {
        CreditsObj.SetActive(true);
        SettingObj.SetActive(false);
        BackButton.SetActive(true);
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
        MainMenu.SetActive(false);
        SettingObj.SetActive(true);
        MainMenuButton.SetActive(true);
        SettingsButton1.SetActive(true);

    }
}

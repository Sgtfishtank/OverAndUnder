using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class UpgradeScript : MonoBehaviour {
    public GameObject[] slowTimeButtons;
    public GameObject[] slowCDButtons;
    public GameObject[] hpButtons;
    public GameObject SlowDurText;
    public GameObject SlowCDText;
    public GameObject HPText;
    public GameObject UpgradeButton;
    public GameObject UpgradeButtonButton;
    public Text score2;
    public Material mat;
    private GameObject[] locks = new GameObject[15];
    private GameObject[] buttonIcons = new GameObject[15];
    private int score;
    private Vector2 current; 
    // Use this for initialization
    void Start ()
    {
        
        UpgradeButton = GameObject.FindGameObjectWithTag("BuyButton");
        UpgradeButton.SetActive(false);
        locks = GameObject.FindGameObjectsWithTag("LocksUpgrades").OrderBy(go => go.name).ToArray();
        buttonIcons = GameObject.FindGameObjectsWithTag("UpgradeBottonMatChange").OrderBy(go => go.name).ToArray();
        selectButtons();
	}
    // Update is called once per frame
    void Update () {
	
	}

    public void selectButtons()
    {
        
        score = ConfigReader.Instance.getValueInt("CrystalsBanked");
        score2.text = score.ToString();
        for (int i = 0; i < slowCDButtons.Length; i++)
        {
            slowCDButtons[i].SetActive(false);
            slowTimeButtons[i].SetActive(false);
        }
        for (int i = 0; i < hpButtons.Length; i++)
        {
            hpButtons[i].SetActive(false);
        }
        int slowcd, slowtime, hp;
        slowcd = ConfigReader.Instance.getValueInt("UpgradeCDLevel") -1;
        slowtime = ConfigReader.Instance.getValueInt("UpgradeDurationLevel") -1;
        hp = ConfigReader.Instance.getValueInt("UpgradeHPLevel") -1;
        slowTimeButtons[Mathf.Max(0, slowtime)].SetActive(true);
        slowCDButtons[Mathf.Max(0,slowcd)].SetActive(true);
        hpButtons[Mathf.Max(0, hp)].SetActive(true);
        if (locks.Length != 0 && locks[0] != null)
        {
            for (int i = 0; i <= Mathf.Max(0, slowtime); i++)
            {
                locks[i].SetActive(false);
            }
            for (int i = 0; i <= Mathf.Max(0, slowcd); i++)
            {
                locks[i + 4].SetActive(false);
            }
            for (int i = 0; i <= Mathf.Max(0, hp); i++)
            {
                locks[i + 8].SetActive(false);
            }
        }
        if (buttonIcons.Length != 0 && buttonIcons[0] != null)
        {
            for (int i = 0; i < Mathf.Max(0, slowtime); i++)
            {
                buttonIcons[i].transform.GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
            for (int i = 0; i < Mathf.Max(0, slowcd); i++)
            {
                buttonIcons[i + 4].transform.GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
            for (int i = 0; i < Mathf.Max(0, hp); i++)
            {
                buttonIcons[i + 8].transform.GetComponent<MeshRenderer>().sharedMaterial = mat;
            }
        }
    }
    public void showUpgrade()
    {
        if (current[0] == 0)
        {
            int cost = ConfigReader.Instance.getValueInt("SlowTimeCostLevel" + (int)current[1]);
            if (cost < score)
            {
                ConfigReader.Instance.changeValue("UpgradeDurationLevel", (int)current[1]+1);
                ConfigReader.Instance.changeValue("CrystalsBanked", ConfigReader.Instance.getValueInt("CrystalsBanked") - cost);
                score2.text = ConfigReader.Instance.getValueInt("CrystalBanked").ToString();
            }
        }
        else if (current[0] == 1)
        {
            int cost = ConfigReader.Instance.getValueInt("SlowCDCostLevel" + (int)current[1]);
            if (cost < score)
            {
                ConfigReader.Instance.changeValue("UpgradeCDLevel", (int)current[1]+1);
                ConfigReader.Instance.changeValue("CrystalsBanked", ConfigReader.Instance.getValueInt("CrystalsBanked") - cost);
                score2.text = ConfigReader.Instance.getValueInt("CrystalBanked").ToString();
            }
        }
        else if (current[0] == 2)
        {
            int cost = ConfigReader.Instance.getValueInt("HPCostLevel" + (int)current[1]);
            if (cost < score)
            {
                ConfigReader.Instance.changeValue("UpgradeHPLevel", (int)current[1]+1);
                ConfigReader.Instance.changeValue("CrystalsBanked", ConfigReader.Instance.getValueInt("CrystalsBanked") - cost);
                score2.text = ConfigReader.Instance.getValueInt("CrystalBanked").ToString();
            }
        }
        selectButtons();
    }
    public void UpgradeSlowBlue(int nr)
    {
        resetButtons();
        SlowDurText.SetActive(true);
        UpgradeButton.SetActive(true);
        UpgradeButtonButton.SetActive(true);
        Text[] a = SlowDurText.transform.GetComponentsInChildren<Text>();
        a[1].text = ConfigReader.Instance.getValueInt("SlowTimeLevel" + (nr-1)) + " sec";
        a[2].text = ConfigReader.Instance.getValueInt("SlowTimeLevel" + nr) + " sec";
        a[3].text = ConfigReader.Instance.getValueInt("SlowTimeCostLevel" + nr).ToString();
        current = new Vector2(0, nr);
    }
    public void UpgradeSlowRed(int nr)
    {
        resetButtons();
        SlowCDText.SetActive(true);
        UpgradeButton.SetActive(true);
        UpgradeButtonButton.SetActive(true);
        Text[] a = SlowCDText.transform.GetComponentsInChildren<Text>();
        a[1].text = ConfigReader.Instance.getValueInt("SlowCDLevel" + (nr - 1)) + " sec";
        a[2].text = ConfigReader.Instance.getValueInt("SlowCDLevel" + nr) + " sec";
        a[3].text = ConfigReader.Instance.getValueInt("SlowCDCostLevel" + nr).ToString();
        current = new Vector2(1, nr);
    }
    public void UpgradeShiled(int nr)
    {
        resetButtons();
        HPText.SetActive(true);
        UpgradeButton.SetActive(true);
        UpgradeButtonButton.SetActive(true);
        Text[] a = HPText.transform.GetComponentsInChildren<Text>();
        a[1].text = ConfigReader.Instance.getValueInt("HPLevel" + (nr - 1)) + " sec";
        a[2].text = ConfigReader.Instance.getValueInt("HPLevel" + nr) + " sec";
        a[3].text = ConfigReader.Instance.getValueInt("HPCostLevel" + nr).ToString();
        current = new Vector2(2, nr);
    }
    private void resetButtons()
    {
        SlowDurText.SetActive(false);
        SlowCDText.SetActive(false);
        HPText.SetActive(false);
        UpgradeButton.SetActive(false);
        UpgradeButtonButton.SetActive(false);

    }
}

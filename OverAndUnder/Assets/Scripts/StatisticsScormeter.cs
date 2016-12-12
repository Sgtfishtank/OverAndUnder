using UnityEngine;
using System.Collections;

public class StatisticsScormeter : MonoBehaviour {
    private float scaleFactor = (1.19f - 0.01587644f) /75;
	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {
        int totalStars = 0;
        int levels = 0;
        for (int i = 1; i < 16; i++)
        {
            if (ConfigReader.Instance.getValue("StarsLevel" + i) > 0)
                levels++;
            totalStars += ConfigReader.Instance.getValue("StarsLevel" + i);

        }
        if (levels == 9 && totalStars > 17)
        {
            levels = 10;
        }
        if (levels == 3 && totalStars > 5)
        {
            levels = 4;
        }
        int upgrades = ConfigReader.Instance.getValue("UpgradeHpLevel") + ConfigReader.Instance.getValue("UpgradeDurationLevel") + ConfigReader.Instance.getValue("UpgradeCDLevel");
        transform.localScale = new Vector3(0.02155756f, 0.02155756f + (scaleFactor * (upgrades + totalStars + levels)), 0.01f);
    }
    // Update is called once per frame
    void Update () {
	
	}

}

using UnityEngine;
using System.Collections;

public class GlobalVariables
{
    // GLOBAL VARIABLES START HERE
    public int UpgradeUnlock = 1;
    public int CrystalsTop = 2;
    public int CrystalsTotal = 1091;
    public int CrystalsBanked = 929;
    public int GamesPlayed = 62;
    public int Healed = 2;
    public int SlowUsed = 9;
    public int ShieldLost = 353;
    public int HeartHits = 6;
    public int UpgradeHPLevel = 2;
    public int UpgradeDurationLevel = 3;
    public int UpgradeCDLevel = 2;
    public int StarsLevel1 = 2;
    public int StarsLevel2 = 2;
    public int StarsLevel3 = 2;
    public int StarsLevel4 = 2;
    public int StarsLevel5 = 2;
    public int StarsLevel6 = 2;
    public int StarsLevel7 = 2;
    public int StarsLevel8 = 2;
    public int StarsLevel9 = 2;
    public int StarsLevel10 = 2;
    public int StarsLevel11 = 2;
    public int StarsLevel12 = 2;
    public int StarsLevel13 = 2;
    public int StarsLevel14 = 2;
    public int StarsLevel15 = 2;
    public int SlowCDCostLevel1 = 500;
    public int SlowCDCostLevel2 = 1000;
    public int SlowCDCostLevel3 = 1500;
    public int SlowCDCostLevel4 = 2000;
    public int SlowTimeCostLevel1 = 500;
    public int SlowTimeCostLevel2 = 1000;
    public int SlowTimeCostLevel3 = 1500;
    public int SlowTimeCostLevel4 = 2000;
    public int HPCostLevel1 = 100;
    public int HPCostLevel2 = 250;
    public int HPCostLevel3 = 500;
    public int HPCostLevel4 = 750;
    public int HPCostLevel5 = 1000;
    public int HPCostLevel6 = 1250;
    public int HPCostLevel7 = 1500;
    public int SlowCDLevel0 = 70;
    public int SlowCDLevel1 = 60;
    public int SlowCDLevel2 = 50;
    public int SlowCDLevel3 = 40;
    public int SlowCDLevel4 = 30;
    public int SlowTimeLevel0 = 6;
    public int SlowTimeLevel1 = 12;
    public int SlowTimeLevel2 = 18;
    public int SlowTimeLevel3 = 24;
    public int SlowTimeLevel4 = 30;
    public int HPLevel0 = 3;
    public int HPLevel1 = 4;
    public int HPLevel2 = 5;
    public int HPLevel3 = 6;
    public int HPLevel4 = 7;
    public int HPLevel5 = 8;
    public int HPLevel6 = 9;
    public int HPLevel7 = 10;
    public int HighScoreLevel1 = 0;
    public int HighScoreLevel2 = 0;
    public int HighScoreLevel3 = 0;
    public int HighScoreLevel4 = 0;
    public int HighScoreLevel5 = 3;
    public int HighScoreLevel6 = 0;
    public int HighScoreLevel7 = 0;
    public int HighScoreLevel8 = 0;
    public int HighScoreLevel9 = 0;
    public int HighScoreLevel10 = 0;
    public int HighScoreLevel11 = 0;
    public int HighScoreLevel12 = 0;
    public int HighScoreLevel13 = 0;
    public int HighScoreLevel14 = 0;
    public int HighScoreLevel15 = 0;
    public int StarRequirementLevel1 = 10;
    public int StarRequirementLevel2 = 20;
    public int StarRequirementLevel3 = 30;
    public int StarRequirementLevel4 = 50;
    public int StarRequirementLevel5 = 70;
    public int StarRequirementLevel6 = 90;
    public int StarRequirementLevel7 = 100;
    public int StarRequirementLevel8 = 120;
    public int StarRequirementLevel9 = 140;
    public int StarRequirementLevel10 = 200;
    public int StarRequirementLevel11 = 400;
    public int StarRequirementLevel12 = 600;
    public int StarRequirementLevel13 = 800;
    public int StarRequirementLevel14 = 1000;
    public int StarRequirementLevel15 = 2500;
    public int SpawnRateLevel1 = 24;
    public int SpawnRateLevel2 = 24;
    public int SpawnRateLevel3 = 24;
    public int SpawnRateLevel4 = 19;
    public int SpawnRateLevel5 = 19;
    public int SpawnRateLevel6 = 19;
    public int SpawnRateLevel7 = 14;
    public int SpawnRateLevel8 = 14;
    public int SpawnRateLevel9 = 14;
    public int SpawnRateLevel10 = 9;
    public int SpawnRateLevel11 = 9;
    public int SpawnRateLevel12 = 9;
    public int SpawnRateLevel13 = 4;
    public int SpawnRateLevel14 = 4;
    public int SpawnRateLevel15 = 4;
    public int CrystalSpeedLevel1 = 30;
    public int CrystalSpeedLevel2 = 30;
    public int CrystalSpeedLevel3 = 30;
    public int CrystalSpeedLevel4 = 25;
    public int CrystalSpeedLevel5 = 25;
    public int CrystalSpeedLevel6 = 25;
    public int CrystalSpeedLevel7 = 20;
    public int CrystalSpeedLevel8 = 20;
    public int CrystalSpeedLevel9 = 20;
    public int CrystalSpeedLevel10 = 15;
    public int CrystalSpeedLevel11 = 15;
    public int CrystalSpeedLevel12 = 15;
    public int CrystalSpeedLevel13 = 10;
    public int CrystalSpeedLevel14 = 10;
    public int CrystalSpeedLevel15 = 10;

    private static GlobalVariables instance = null;

    public static GlobalVariables Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GlobalVariables();
            }
            return instance;
        }
    }

	public int GetCrystalSpeedLevel(int level)
	{
		switch (level)
		{
			case 1:
                return CrystalSpeedLevel1;
            case 2:
                return CrystalSpeedLevel2;
            case 3:
                return CrystalSpeedLevel3;
            case 4:
                return CrystalSpeedLevel4;
            case 5:
                return CrystalSpeedLevel5;
            case 6:
                return CrystalSpeedLevel6;
            case 7:
                return CrystalSpeedLevel7;
            case 8:
                return CrystalSpeedLevel8;
            case 9:
                return CrystalSpeedLevel9;
            case 10:
                return CrystalSpeedLevel10;
            case 11:
                return CrystalSpeedLevel11;
            case 12:
                return CrystalSpeedLevel12;
            case 13:
                return CrystalSpeedLevel13;
            case 14:
                return CrystalSpeedLevel14;
            case 15:
                return CrystalSpeedLevel15;
            default:
                return -1;
		}
	}
    public int GetCrystalSpawnRateLevel(int level)
    {
        switch (level)
        {
            case 1:
                return SpawnRateLevel1;
            case 2:
                return SpawnRateLevel2;
            case 3:
                return SpawnRateLevel3;
            case 4:
                return SpawnRateLevel4;
            case 5:
                return SpawnRateLevel5;
            case 6:
                return SpawnRateLevel6;
            case 7:
                return SpawnRateLevel7;
            case 8:
                return SpawnRateLevel8;
            case 9:
                return SpawnRateLevel9;
            case 10:
                return SpawnRateLevel10;
            case 11:
                return SpawnRateLevel11;
            case 12:
                return SpawnRateLevel12;
            case 13:
                return SpawnRateLevel13;
            case 14:
                return SpawnRateLevel14;
            case 15:
                return SpawnRateLevel15;
            default:
                return -1;
        }
    }
    public int GetStarReq(int level)
    {
        switch (level)
        {
            case 1:
                return StarRequirementLevel1;
            case 2:
                return StarRequirementLevel2;
            case 3:
                return StarRequirementLevel3;
            case 4:
                return StarRequirementLevel4;
            case 5:
                return StarRequirementLevel5;
            case 6:
                return StarRequirementLevel6;
            case 7:
                return StarRequirementLevel7;
            case 8:
                return StarRequirementLevel8;
            case 9:
                return StarRequirementLevel9;
            case 10:
                return StarRequirementLevel10;
            case 11:
                return StarRequirementLevel11;
            case 12:
                return StarRequirementLevel12;
            case 13:
                return StarRequirementLevel13;
            case 14:
                return StarRequirementLevel14;
            case 15:
                return StarRequirementLevel15;
            default:
                return -1;
        }
    }
    public int GetStarlevel(int level)
    {
        switch (level)
        {
            case 1:
                return StarsLevel1;
            case 2:
                return StarsLevel2;
            case 3:
                return StarsLevel3;
            case 4:
                return StarsLevel4;
            case 5:
                return StarsLevel5;
            case 6:
                return StarsLevel6;
            case 7:
                return StarsLevel7;
            case 8:
                return StarsLevel8;
            case 9:
                return StarsLevel9;
            case 10:
                return StarsLevel10;
            case 11:
                return StarsLevel11;
            case 12:
                return StarsLevel12;
            case 13:
                return StarsLevel13;
            case 14:
                return StarsLevel14;
            case 15:
                return StarsLevel15;
            default:
                return -1;
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> boxes;
    public List<GameObject> cores;
    public List<GameObject> balls;
    public List<Transform> boxPoints;
    public List<Transform> BallSpawnPoints;
    public ScrollingTexture[] lanetextscript;
    public GameObject[] covers;
    public List<int> destroyedLanes;
    public GameObject[] scoreMeterStars;
    public GameObject[] healBox;
    public GameObject[] SkillStuff;
    public GameObject scoreMeterBlue;
    public GameObject blueBox;
    public GameObject redBox;
    public GameObject ghostBox;
    public GameObject blueBall;
    public GameObject redBall;
    public GameObject healEffect;
    public GameObject scoreStreakEffect;
    public GameObject coreObj;
    private GameObject cameraGO;
    
    public int blueScore;
    public int redScore;
    public int multiplier;
    public float spawnRate;
    public float slowSpeed;
    public float normalspeed;
    public Color defaultcolor;
    public Color healemissive;
    public bool GameOver = false;

    public int currentLevel;
    private Abilitys AM;
    private GameObject healslot;
    internal float lastSpawn;
    private float speedUpTime;
    private float spawnRateTime;
    private float scalefactor = (1.175139f - 0.01587644f) /300;
    private float posfactor = (0.055496f- 0.05536f) /300;
    private int scorestreak;
    private int scorestreakLimit;
    internal bool Countdown = true;

    void Start()
    {
        cameraGO = GameObject.Find("camera_main");
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);
        ColorUtility.TryParseHtmlString("#002D0000", out healemissive);
        healEffect.SetActive(false);
        scoreStreakEffect.SetActive(false);

        AM = transform.GetComponent<Abilitys>();

        speedUpTime = Time.time + 10;

        healslot = GameObject.Find("mesh_heal_slot_new");
        
        clear();
        initalize();
    }
    public void Reset(int level)
    {
        currentLevel = level;
        for (int i = 0; i < healBox.Length; i++)
        {
            healBox[i].SetActive(true);
        }
        for (int i = 0; i < SkillStuff.Length; i++)
        {
            SkillStuff[i].SetActive(true);
        }
        if (currentLevel < 2)
        {
            for (int i = 0; i < healBox.Length; i++)
            {
                healBox[i].SetActive(false);
            }
            for (int i = 0; i < SkillStuff.Length; i++)
            {
                SkillStuff[i].SetActive(false);
            }
        }
        if(currentLevel < 3)
        {
            for (int i = 0; i < SkillStuff.Length; i++)
            {
                SkillStuff[i].SetActive(false);
            }
        }
        clear();
        initalize();
        blueScore = 0;
        redScore = 0;
        normalspeed = 0.75f;
        spawnRate = 1.5f;
        destroyedLanes.Clear();
        GameOver = false;
        speedUpTime = Time.time + 10;
        scorestreakLimit = 20;
        scorestreak = 0;
        for (int i = 0; i < lanetextscript.Length; i++)
        {
            lanetextscript[i].scrollSpeed = -0.144f;
        }
        for (int i = 0; i < scoreMeterStars.Length; i++)
        {
            scoreMeterStars[i].SetActive(false);
        }
        if (currentLevel < 4)
        {
            covers[0].SetActive(true);
            covers[1].SetActive(false);
            destroyedLanes.Add(0);
            destroyedLanes.Add(1);
            destroyedLanes.Add(3);
            destroyedLanes.Add(4);
        }
        else if(currentLevel > 3 && currentLevel < 10)
        {
            covers[0].SetActive(false);
            covers[1].SetActive(true);
            destroyedLanes.Add(0);
            destroyedLanes.Add(3);
        }
        else
        {
            covers[0].SetActive(false);
            covers[1].SetActive(false);
        }
    }
    public void clear()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            DestroyImmediate(boxes[i]);
        }
        for (int i = 0; i < balls.Count; i++)
        {
            DestroyImmediate(balls[i]);
        }
        for (int i = 0; i < cores.Count; i++)
        {
            DestroyImmediate(cores[i]);
        }
        boxes.Clear();
        balls.Clear();
        cores.Clear();
    }
    void initalize()
    {
        Transform parent = GameObject.Find("Ball Objects").transform;
        lanetextscript = transform.GetComponentsInChildren<Transform>().Where(x => x.tag == "Lane").Select(x => x.transform.GetComponent<ScrollingTexture>()).ToArray();
        
        for (int i = 0; i < boxPoints.Count; i++)
        {
            if (i < 3)
                boxes.Add(Instantiate(blueBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            else if (i > 2 && i < 6)
                boxes.Add(Instantiate(redBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            else
            {
                boxes.Add(Instantiate(ghostBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            }
            if (i != 6)
                boxes[i].transform.GetComponent<Box>().StartPos(i); 
        }
        if (currentLevel < 4)
        {
            boxes[0].SetActive(false);
            boxes[1].SetActive(false);
            boxes[3].SetActive(false);
            boxes[4].SetActive(false);
            if(currentLevel == 1)
                boxes.RemoveAt(6);
        }
        for (int i = 0; i < boxPoints.Count - 1; i++)
        {
            cores.Add(Instantiate(coreObj, boxPoints[i].position + new Vector3(0, 0, 0.1f), Quaternion.identity) as GameObject);
        }
        for (int i = 0; i < cores.Count; i++)
        {
            cores[i].GetComponent<Core>().setLevel(currentLevel);
        }
        for (int i = 0; i < 20; i++)
        {
            if (i < 10)
            {
                balls.Add(Instantiate(blueBall, Vector3.zero, Quaternion.identity) as GameObject);
            }
            else if (i < 20 && i > 9)
            {
                balls.Add(Instantiate(redBall, Vector3.zero, Quaternion.identity) as GameObject);

            }
            balls[i].SetActive(false);
            balls[i].transform.parent = parent;
        }
    }
    void Update()
    {
        if (Countdown)
            return;
        if (GameOver)
        {
            return;
        }
        if (destroyedLanes.Count == 6)
        {
            for (int i = 0; i < lanetextscript.Length; i++)
            {
                lanetextscript[i].scrollSpeed = 0;
            }
            for(int i = 0; i < balls.Count;i++)
            {
                balls[i].SetActive(false);
            }
            GameOver = true;
            return;
        }

        if (Time.time > lastSpawn)
        {
            if (UnityEngine.Random.Range(0, 1) == 0)//pattern
            {
                int spawnpos = getLane();
                int color = UnityEngine.Random.Range(0, 2);
                if (AM.abilityActive && (AM.abilitysInLane[0].y == 1 || AM.abilitysInLane[2].y == 1))
                {
                    abilitySpawning(spawnpos, color);
                }
                else
                {
                    spawnNormal(spawnpos, normalspeed, color, false);
                }
            }
            lastSpawn = Time.time + spawnRate;
        }
        
        if(speedUpTime < Time.time)
        {
            if (AM.abilityActive && (AM.abilitysInLane[0].y == 1))
            {
                speedUpTime = Time.time + 24;
                return;
            }
            speedUp();
            speedUpTime = Time.time + 24;
            for (int i = 0; i < lanetextscript.Length; i++)
            {
                if(!destroyedLanes.Contains(i))
                    lanetextscript[i].scrollSpeed -= 0.0096f;
            }
        }
        if (spawnRateTime < Time.time)
        {
            if (AM.abilityActive && (AM.abilitysInLane[0].y == 1))
            {
                spawnRateTime = Time.time + 30;
                return;
            }
            spawnrateUp();
            spawnRateTime = Time.time + 30f;
        }
        if (currentLevel > 1)
        {
            if (boxes.Last().tag == "Blue Box" || boxes.Last().tag == "Red Box")
            {
                healEffect.SetActive(true);
                if (healslot.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor") != healemissive)
                    healslot.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", healemissive);
            }
            else if (healEffect.activeSelf)
            {
                healEffect.SetActive(false);
                if (healslot.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor") == healemissive)
                    healslot.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultcolor);
            }
        }
        if ((blueScore +redScore) < 301)
        {
            scoreMeterBlue.transform.localPosition = new Vector3(scoreMeterBlue.transform.localPosition.x, 0.055496f + (posfactor * (redScore + blueScore)), scoreMeterBlue.transform.localPosition.z);
            scoreMeterBlue.transform.localScale = new Vector3(scoreMeterBlue.transform.localScale.x, 0.01587644f + (scalefactor * (redScore + blueScore)), scoreMeterBlue.transform.localScale.z);
        }
        if ((blueScore + redScore) > ConfigReader.Instance.getValue("StarRequirementLevel" + currentLevel)-1)
            scoreMeterStars[0].SetActive(true);
        if ((blueScore + redScore) > (ConfigReader.Instance.getValue("StarRequirementLevel" + currentLevel)*2)-1)
                scoreMeterStars[1].SetActive(true);
        if ((blueScore + redScore) > (ConfigReader.Instance.getValue("StarRequirementLevel" + currentLevel)*3)-1)
            scoreMeterStars[2].SetActive(true);
        if(scorestreak > scorestreakLimit)
        {
            scorestreakLimit += 20;
            scoreStreakEffect.SetActive(true);
            
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].GetComponent<Box>().currentMultiplier++;
            }
            cameraGO.GetComponentInChildren<UI>().activateScorstreak(boxes[0].GetComponent<Box>().currentMultiplier);
        }
    }
    void speedUp()
    {
        if (normalspeed < 1.96f)
        {
            normalspeed += 0.05f;
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].GetComponent<Ball>().lane < 3)
                    balls[i].transform.GetComponent<Rigidbody>().velocity += new Vector3(0, -0.05f, 0);
                else
                    balls[i].transform.GetComponent<Rigidbody>().velocity += new Vector3(0, 0.05f, 0);
            }
        }
    }
    void spawnrateUp()
    {
        if (spawnRate > 0.54f)
            spawnRate -= 0.05f;
    }
    public void unLockBoxes(int slot)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].GetComponent<Box>().canMove();
        }
    }
    public void LockBoxes(int slot)
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if(i != slot)
                boxes[i].GetComponent<Box>().cantMove();
        }
    }
    int getLane()
    {
        int temp = UnityEngine.Random.Range(0,6);
        if(destroyedLanes.Contains(temp))
        {
            temp = getLane();
        } 
        return temp;
    }
    public void destroyLane(int slot)
    {

        if (slot == 6)
        {
            int temp = slot;
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].tag == "Untagged")
                    slot = i;
            }
            boxes[temp].GetComponent<Box>().newPos(slot);
        }
        if (destroyedLanes.Contains(slot))
            return;
        destroyedLanes.Add(slot);
        lanetextscript[slot].scrollSpeed = 0;
        cores[slot].SetActive(false);
        for (int i = 0; i <balls.Count; i++)
        {
            if(balls[i].transform.GetComponent<Ball>().lane == slot)
            {
                balls[i].SetActive(false);
            }
        }
    }
    void spawnNormal(int lane, float speed, int color, bool multi)
    {
        int ball = 0;
        if (color == 0)
        {
            ball = findInactiveBlue();
        }
        else if(color == 1)
        {
            ball = findInactiveRed();
        }
        
        balls[ball].transform.position = BallSpawnPoints[lane].position;
        balls[ball].transform.rotation = UnityEngine.Random.rotation;
        balls[ball].transform.GetComponent<Ball>().lane = lane;
        if (balls[ball].tag == "Red")
            balls[ball].GetComponentInChildren<ParticleSystem>().startColor = AM.red;
        else
            balls[ball].GetComponentInChildren<ParticleSystem>().startColor = AM.blue;
        if(multi)
            balls[ball].GetComponentInChildren<ParticleSystem>().startColor = AM.purple;
        if (lane < 3)
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, -speed, 0);
        else
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
        balls[ball].SetActive(true);
    }
    public void abilitySpawning(int lane, int color)
    {
        if(AM.abilitysInLane[2].y == 1)
            spawnNormal(lane, normalspeed, color, true);
       else
            spawnNormal(lane, normalspeed/4, color, false);
        
    }
    public void patternSpawn(int lane, float speed, int color)
    {

        /*
        size of pattern
        in pattern
        type of pattern
        varibles:
        count
        pos

        inPatter = true;
        spawnsInPattern;
        spawnedInPatter;
        posToSpawn;
        colorToSpawn
        */








        int ball = 0;
        if (color == 0)
        {
            ball = findInactiveBlue();
        }
        else if (color == 1)
        {
            ball = findInactiveRed();
        }

        balls[ball].transform.position = BallSpawnPoints[lane].position;
        balls[ball].transform.rotation = UnityEngine.Random.rotation;
        balls[ball].transform.GetComponent<Ball>().lane = lane;
        if (balls[ball].tag == "Red")
            balls[ball].GetComponentInChildren<ParticleSystem>().startColor = AM.red;
        else
            balls[ball].GetComponentInChildren<ParticleSystem>().startColor = AM.blue;
        if (lane < 3)
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, -speed, 0);
        else
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
        balls[ball].SetActive(true);
    }
    int findInactiveBlue()
    {
        for (int i = 0; i < balls.Count/2; i++)
        {
            if (!balls[i].activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
    int findInactiveRed()
    {
        for (int i = balls.Count/2; i < balls.Count; i++)
        {
            if (!balls[i].activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
    public void addRedScore(int score)
    {
        scorestreak++;
        redScore += score;
    }
    public void addBlueScore(int score)
    {
        scorestreak++;
        blueScore += score;
    }
    public void swapBox(int CurrentPos, int newPos)
    {
        GameObject temp = boxes[newPos];
        boxes[newPos] = boxes[CurrentPos];
        boxes[CurrentPos] = temp;
        boxes[CurrentPos].transform.GetComponent<Box>().newPos(CurrentPos);
    }
    public void resetScoreStreak()
    {
        scorestreak = 0;
        scorestreakLimit = 20;
        scoreStreakEffect.SetActive(false);
        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].GetComponent<Box>().currentMultiplier = 1;
        }
    }
}
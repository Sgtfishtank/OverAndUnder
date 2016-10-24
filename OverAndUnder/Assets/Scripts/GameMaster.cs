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
    public List<int> destroyedLanes;
    public GameObject blueBox;
    public GameObject redBox;
    public GameObject ghostBox;
    public GameObject blueBall;
    public GameObject redBall;
    public GameObject healEffect;
    public GameObject coreObj;
    
    public int blueScore;
    public int redScore;
    public int multiplier;
    public float spawnRate;
    public float slowSpeed;
    public float normalspeed;
    public Color defaultcolor;
    public Color healemissive;
    public bool GameOver = false;


    private Abilitys AM;
    private GameObject healslot;
    private float lastSpawn;
    private float speedUpTime;
    private float spawnRateTime;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);
        ColorUtility.TryParseHtmlString("#002D0000", out healemissive);
        healEffect.SetActive(false);

        AM = transform.GetComponent<Abilitys>();

        speedUpTime = Time.time + 10;

        healslot = GameObject.Find("mesh_heal_slot_new");
        clear();
        initalize();
    }
    public void Reset()
    {
        clear();
        initalize();
        blueScore = 0;
        redScore = 0;
        normalspeed = 0.75f;
        spawnRate = 1.5f;
        destroyedLanes.Clear();
        GameOver = false;
        speedUpTime = Time.time + 10;
        for (int i = 0; i < lanetextscript.Length; i++)
        {
            lanetextscript[i].scrollSpeed = -0.144f;
            lanetextscript[i].scrollSpeed2 = -0.15f;
        }
    }
    void clear()
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
            else if (i < 6 && i > 2)
                boxes.Add(Instantiate(redBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            else
            {
                boxes.Add(Instantiate(ghostBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            }
            if (i != 6)
                boxes[i].transform.GetComponent<Box>().StartPos(i);
        }
        for (int i = 0; i < boxPoints.Count - 1; i++)
        {
            cores.Add(Instantiate(coreObj, boxPoints[i].position + new Vector3(0, 0, 0.1f), Quaternion.identity) as GameObject);
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
        if(GameOver)
        {
            return;
        }
        if (destroyedLanes.Count == 6)
        {
            for (int i = 0; i < lanetextscript.Length; i++)
            {
                lanetextscript[i].scrollSpeed = 0;
                lanetextscript[i].scrollSpeed2 = 0;
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
            speedUp();
            speedUpTime = Time.time + 24;
            for (int i = 0; i < lanetextscript.Length; i++)
            {
                if(!destroyedLanes.Contains(i))
                lanetextscript[i].scrollSpeed -= 0.0096f;
                lanetextscript[i].scrollSpeed2 -= 0.01f;
            }
        }
        if (spawnRateTime < Time.time)
        {
            spawnrateUp();
            spawnRateTime = Time.time + 30f;
        }

        if (boxes[6].tag == "Blue Box" || boxes[6].tag == "Red Box")
        {
            healEffect.SetActive(true);
            if(healslot.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor") != healemissive)
                healslot.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", healemissive);
        }
        else  if(healEffect.activeSelf)
        {
            healEffect.SetActive(false);
            if (healslot.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor") == healemissive)
                healslot.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultcolor);
        }
        

    }
    void speedUp()
    {
        if(normalspeed < 1.96f)
            normalspeed += 0.05f;
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
        destroyedLanes.Add(slot);
        lanetextscript[slot].scrollSpeed = 0;
        lanetextscript[slot].scrollSpeed2 = 0;
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
            spawnNormal(lane, normalspeed/2, color, false);
        
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
    public void addRedScore(int lane)
    {
        if (AM.abilitysInLane[2].y == 1 && AM.abilitysInLane[2].z == lane)
            redScore += 1 * multiplier;
        else
            redScore++;
    }
    public void addBlueScore(int lane)
    {
        if (AM.abilitysInLane[2].y == 1 && AM.abilitysInLane[2].z == lane)
            blueScore += 1 * multiplier;
        else
            blueScore++;
    }
    public void swapBox(int CurrentPos, int newPos)
    {
        GameObject temp = boxes[newPos];
        boxes[newPos] = boxes[CurrentPos];
        boxes[CurrentPos] = temp;
        boxes[CurrentPos].transform.GetComponent<Box>().newPos(CurrentPos);
    }
}
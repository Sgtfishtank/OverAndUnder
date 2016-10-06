using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> boxes;
    public List<GameObject> balls;
    public List<Transform> boxPoints;
    public List<Transform> BallSpawnPoints;
    public ScrollingTexture[] lanetextscript;
    public MeshRenderer[] lanerenders;
    public GameObject blueBox;
    public GameObject redBox;
    public GameObject ghostBox;
    public GameObject blueBall;
    public GameObject redBall;
    public GameObject WallObj;
    public int score;
    public float spawnRate;
    private float lastSpawn;
    public bool abilityActive;
    public float SlowDuration;
    public float WallDuration;
    public float MultiDuration;
    public float NeutralDuration;
    private float durationTime;
    public Abilitys currentActive;
    public int activeLane;
    public float slowSpeed;
    public float normalspeed;
    public int multiplier;
    public bool selectingAbility = false;
    public Color defaultcolor;
    public Color slowcolor;
    public Color wallcolor;
    public Color multicolor;
    public Color neutcolor;
    public GameObject healEffect;

    public enum Abilitys
    {
        SLOW,WALL,MULTIPLIER,NEUTRAL,NONE
    };

    
    

    void Start()
    {
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);
        ColorUtility.TryParseHtmlString("#121E3300", out slowcolor);
        ColorUtility.TryParseHtmlString("#330F0F00", out wallcolor);
        ColorUtility.TryParseHtmlString("#300B3300", out multicolor);
        ColorUtility.TryParseHtmlString("#33310E00", out neutcolor);
        healEffect.SetActive(false);

        Transform parent = GameObject.Find("Ball Objects").transform;
        lanetextscript = transform.GetComponentsInChildren<Transform>().Where(x => x.tag == "Lane").Select(x => x.transform.GetComponent<ScrollingTexture>()).ToArray();
        lanerenders = transform.GetComponentsInChildren<Transform>().Where(x => x.tag == "Lane").Select(x => x.transform.GetComponent<MeshRenderer>()).ToArray();

        for (int i = 0; i < boxPoints.Count; i++)
        {
            if (i < 3)
                boxes.Add(Instantiate(blueBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            else if (i <6 && i >2 )
                boxes.Add(Instantiate(redBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            else
            {
                boxes.Add(Instantiate(ghostBox, boxPoints[i].position, Quaternion.identity) as GameObject);
            }
            if(i != 6)
                boxes[i].transform.GetComponent<Box>().StartPos(i);
        }
          for (int i = 0; i < 20; i++)
          {
              if (i < 10)
              {
                  balls.Add(Instantiate(blueBall, Vector3.zero, Quaternion.identity) as GameObject);
              }
              else if(i<20 && i> 9)
              {
                  balls.Add(Instantiate(redBall, Vector3.zero, Quaternion.identity) as GameObject);

              }
              balls[i].SetActive(false);
              balls[i].transform.parent = parent;
          }
    }

    void Update()
    {
        if(Time.time > lastSpawn)
        {
            if(durationTime < Time.time && abilityActive)
            {
                switch (currentActive)
                {
                    case Abilitys.SLOW:
                        for (int i = 0; i < balls.Count; i++)
                        {
                            if (balls[i].activeSelf && balls[i].transform.GetComponent<Ball>().lane == activeLane)
                            {
                                balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, balls[i].GetComponent<Rigidbody>().velocity.y * 2, 0);
                            }
                        }
                        lanetextscript[activeLane].scrollSpeed *= 2;
                        lanetextscript[activeLane].scrollSpeed2 *= 2;
                        break;
                    case Abilitys.WALL:
                        break;
                    case Abilitys.NEUTRAL:
                        break;
                    default:
                        break;
                }
                lanerenders[activeLane].material.SetColor("_EmissionColor", defaultcolor);
                abilityActive = false;
                activeLane = 10;
                currentActive = Abilitys.NONE;
            }
            if(UnityEngine.Random.Range(0,1) ==0)//pattern
            {
                int spawnpos = UnityEngine.Random.Range(0, 5);
                int color = UnityEngine.Random.Range(0, 2);
                if(abilityActive && activeLane == spawnpos)
                {
                    abilitySpawning(spawnpos, color);
                }
                else
                {
                    spawnNormal(spawnpos, normalspeed, color);
                }
            }
            lastSpawn = Time.time + spawnRate;
        }
        if(boxes[6].tag == "Blue Box" || boxes[6].tag == "Red Box")
        {
            healEffect.SetActive(true);
        }
        else  if(healEffect.activeSelf)
        {
            healEffect.SetActive(false);
        }

    }
    public void highlightLanes(Abilitys ability)
    {
        currentActive = ability;
        selectingAbility = true;
    }
    public void unlightLanes()
    {
        currentActive = Abilitys.NONE;
        selectingAbility = false;
    }
    public void setLane(int lane)
    {
        
        if (selectingAbility)
        {
            activeLane = lane;
            activateAbility(currentActive, lane);
        }
    }
    void spawnNormal(int lane, float speed, int color)
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
        if (lane < 3)
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, -speed, 0);
        else
            balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
        balls[ball].SetActive(true);
    }
    public void abilitySpawning(int lane, int color)
    {
        switch (currentActive)
        {
            case Abilitys.SLOW:
                spawnNormal(lane, slowSpeed, color);
                break;
            case Abilitys.NEUTRAL:
                spawnNormal(lane, normalspeed, 0);
                break;
            default:
                break;
        }
    }
    public void activateAbility(GameMaster.Abilitys a, int lane)
    {
        abilityActive = true;
        selectingAbility = false;
        currentActive = a;
        activeLane = lane;
        
        switch (a)
        {
            case Abilitys.SLOW:
                durationTime = Time.time + SlowDuration;
                for (int i = 0; i < balls.Count; i++)
                {
                    if(balls[i].activeSelf && balls[i].transform.GetComponent<Ball>().lane == lane)
                    {
                        balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, balls[i].GetComponent<Rigidbody>().velocity.y/2, 0);
                    }
                }
                lanetextscript[activeLane].scrollSpeed /= 2;
                lanetextscript[activeLane].scrollSpeed2 /= 2;
                
                lanerenders[activeLane].material.SetColor("_EmissionColor", slowcolor);

                break;
            case Abilitys.WALL:
                durationTime = Time.time + WallDuration;
                switch (lane)
                {
                    case 0:
                        WallObj.transform.position = new Vector3(-2.01f, 1, 0);
                        break;
                    case 1:
                        WallObj.transform.position = new Vector3(-0.59f, 1, 0);
                        break;
                    case 2:
                        WallObj.transform.position = new Vector3(0.81f, 1, 0);
                        break;
                    case 3:
                        WallObj.transform.position = new Vector3(-2.01f, -1, 0);
                        break;
                    case 4:
                        WallObj.transform.position = new Vector3(-0.59f, -1, 0);
                        break;
                    case 5:
                        WallObj.transform.position = new Vector3(0.81f, -1, 0);
                        break;
                    default:
                        break;
                }
                WallObj.SetActive(true);
                lanerenders[activeLane].material.SetColor("_EmissionColor", wallcolor);
                break;
            case Abilitys.MULTIPLIER:
                durationTime = Time.time + MultiDuration;
                lanerenders[activeLane].material.SetColor("_EmissionColor", multicolor);
                multiplier = 3;
                break;
            case Abilitys.NEUTRAL:
                durationTime = Time.time + NeutralDuration;
                lanerenders[activeLane].material.SetColor("_EmissionColor", neutcolor);
                break;
            default:

                break;
        }

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
    public void addScore(int lane)
    {
        if (currentActive == Abilitys.MULTIPLIER && activeLane == lane)
            score += 1 * multiplier;
        else
            score++;
    }
    public void swapBox(int CurrentPos, int newPos)
    {
        GameObject temp = boxes[newPos];
        boxes[newPos] = boxes[CurrentPos];
        boxes[CurrentPos] = temp;
        boxes[CurrentPos].transform.GetComponent<Box>().newPos(CurrentPos);
    }
}


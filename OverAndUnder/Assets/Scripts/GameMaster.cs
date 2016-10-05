using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> boxes;
    public List<GameObject> balls;
    public List<Transform> boxPoints;
    public List<Transform> BallSpawnPoints;
    public GameObject blueBox;
    public GameObject redBox;
    public GameObject ghostBox;
    public GameObject blueBall;
    public GameObject redBall;
    public int score;
    public float spawnRate;
    private float lastSpawn;
    public bool abilityActive;
    public float SlowDuration;
    public float WallDuration;
    public float MultiDuration;
    public float NeutralDuration;
    public float durationTime;
    public Abilitys currentActive;
    public int activeLane;
    public float slowSpeed;
    public float normalspeed;
    public int multiplier;

    public enum Abilitys
    {
        SLOW,WALL,MULTIPLIER,NEUTRAL,NONE
    };

    
    

    void Start()
    {
        Transform parent = GameObject.Find("Ball Objects").transform;

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
          for (int i = 0; i < 30; i++)
          {
              if (i < 15)
              {
                  balls.Add(Instantiate(blueBall, Vector3.zero, Quaternion.identity) as GameObject);
              }
              else if(i<30 && i> 14)
              {
                  balls.Add(Instantiate(redBall, Vector3.zero, Quaternion.identity) as GameObject);

              }
            else 
            {
               // balls.Add(Instantiate(Ball, Vector3.zero, Quaternion.identity) as GameObject);

            }
            balls[i].SetActive(false);
              balls[i].transform.parent = parent;
          }
    }

    void Update()
    {
        if(Time.time > lastSpawn)
        {
            if(durationTime < Time.time)
            {
                abilityActive = false;
                multiplier = 1;
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
        else
        {
            ball = findInactiveGold();
        }
        balls[ball].transform.position = BallSpawnPoints[lane].position;
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
                spawnNormal(lane, slowSpeed, 2);
                break;
            default:
                break;
        }
    }
    public void activateAbility(GameMaster.Abilitys a, int lane)
    {
        abilityActive = true;
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
                        balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, slowSpeed, 0);
                    }
                }
                break;
            case Abilitys.WALL:
                durationTime = Time.time + WallDuration;
                break;
            case Abilitys.MULTIPLIER:
                durationTime = Time.time + MultiDuration;
                multiplier = 3;
                break;
            case Abilitys.NEUTRAL:
                durationTime = Time.time + NeutralDuration;
                break;
            default:

                break;
        }

    }
    int findInactiveBlue()
    {
        for (int i = 0; i < 15 - 1; i++)
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
        for (int i = 15; i < 30; i++)
        {
            if (!balls[i].activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
    int findInactiveGold()
    {
        for (int i = 30; i < 39; i++)
        {
            if (!balls[i].activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
    public void addScore()
    {
        score += 1 * multiplier;
    }
    public void swapBox(int CurrentPos, int newPos)
    {
        GameObject temp = boxes[newPos];
        boxes[newPos] = boxes[CurrentPos];
        boxes[CurrentPos] = temp;
        boxes[CurrentPos].transform.GetComponent<Box>().newPos(CurrentPos);
    }
}


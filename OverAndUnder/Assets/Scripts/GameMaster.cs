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
    private float lastSpawn;
    public float spawnRate;

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
          for (int i = 0; i < 50; i++)
          {
              if (i < 25)
              {
                  balls.Add(Instantiate(blueBall, Vector3.zero, Quaternion.identity) as GameObject);
              }
              else
              {
                  balls.Add(Instantiate(redBall, Vector3.zero, Quaternion.identity) as GameObject);

              }
              balls[i].SetActive(false);
              balls[i].transform.parent = parent;
          }
    }

    void Update()
    {
        /*
        spawnrate
        color
        pattern
        pos
        speed

        */
        if(Time.time > lastSpawn)
        {
            if(UnityEngine.Random.Range(0,1) ==0)//pattern
            {
                int spawnpos = UnityEngine.Random.Range(0, 5);
                if (UnityEngine.Random.Range(0, 2) == 0)//collor blue
                {
                    int ball = findInactiveBlue();

                    balls[ball].transform.position = BallSpawnPoints[spawnpos].position;
                    if (spawnpos < 3)
                        balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
                    else
                        balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                    balls[ball].SetActive(true);
                }
                else //collor red
                {
                    int ball = findInactiveRed();

                    balls[ball].transform.position = BallSpawnPoints[spawnpos].position;
                    if (spawnpos < 3)
                        balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
                    else
                        balls[ball].GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
                    balls[ball].SetActive(true);
                }
            }

            lastSpawn = Time.time + spawnRate;
        }
    }

    int findInactiveBlue()
    {
        for (int i = 0; i < balls.Count / 2 - 1; i++)
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


    public void addScore()
    {
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


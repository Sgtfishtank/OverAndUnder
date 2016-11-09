using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Abilitys : MonoBehaviour
{
    public MeshRenderer[] lanerenders;
    public Vector3[] abilitysInLane;
    public AbilityButton[] abb;
    public GameObject SlowObj;
    
    public bool abilityActive;
    public float SlowDuration;
    public float slowCD;
    
    public AbilitysEnum currentActive;
    public bool selectingAbility = false;
    /*private Color defaultcolor;
    public Color slowcolor;*/
    public Color blue;
    public Color red;
    public Color purple;
    public Material defaultMat;
    public Material slowMat;

    internal int slowRemaning;

    private bool blink;

    private float blinkInterwall;

    private int currentLevel;

    private GameMaster GM;
    private float slowTime;

    public enum AbilitysEnum
    {
        SLOW, WALL, MULTIPLIER, SWITCH, NONE
    };
    void Start()
    {
        //ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);

        SlowObj = Instantiate(SlowObj, Vector3.zero, Quaternion.identity) as GameObject;
        SlowObj.SetActive(false);

        GM = transform.GetComponent<GameMaster>();

        lanerenders = transform.GetComponentsInChildren<Transform>().Where(x => x.tag == "Lane").Select(x => x.transform.GetComponent<MeshRenderer>()).ToArray();
        initalize();
    }
    public void Reset(int level)
    {
        currentLevel = level;
        initalize();
        slowTime = 0;
    }
    void initalize()
    {
        abb = transform.GetComponentsInChildren<AbilityButton>();
    }
    void Update()
    {
        if(blink)
        {
            if(blinkInterwall < Time.time)
            {
                print(blinkInterwall);
                for (int i = 0; i < GM.lanetextscript.Length; i++)
                {
                    print(lanerenders[i].material);
                    if(lanerenders[i].material.name != defaultMat.name + " (Instance)")
                        lanerenders[i].sharedMaterial = defaultMat;
                    else
                        lanerenders[i].sharedMaterial = slowMat;
                }
                blinkInterwall = Time.time + 0.5f;
            }
        }
        if(abilitysInLane[0].y == 1 && abilitysInLane[0].x - Time.time < 3 && !blink)
        {
            blink = true;
            blinkInterwall = Time.time + 0.5f;
        }
        if (abilitysInLane[0].y == 1 && abilitysInLane[0].x < Time.time)
        {
            slowReset();

        }
        if (currentLevel > 2)
        {
            if (slowTime < Time.time)
            {
                abb[0].isCD(false);
            }
        }
    }
    void slowReset()
    {
        blink = false;
        for (int i = 0; i < GM.balls.Count; i++)
        {
            GM.balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, GM.balls[i].GetComponent<Rigidbody>().velocity.y * 4, 0);
        }
        for (int i = 0; i < GM.lanetextscript.Length; i++)
        {
            GM.lanetextscript[i].scrollSpeed *= 4;
            lanerenders[i].sharedMaterial = defaultMat;
        }
        GM.spawnRate /= 2f;
        slowTime = Time.time + slowCD;
        slowRemaning = Mathf.FloorToInt(slowTime);
        abb[0].isCD(true);
        abb[0].active = false;
        abb[0].setColor();
        abilitysInLane[0].y = 0;
        SlowObj.SetActive(false);
    }
public void activateAbility(AbilitysEnum a, int lane)
    {
        abilityActive = true;
        selectingAbility = false;
        switch (a)
        {
            case AbilitysEnum.SLOW:
                if (slowTime > Time.time || abilitysInLane[0].x > Time.time)
                    return;
                abilitysInLane[0].x = Time.time + SlowDuration;
                abilitysInLane[0].y = 1;
                abilitysInLane[0].z = lane;
                slowTime = Mathf.Infinity;
                slowRemaning = Mathf.FloorToInt(abilitysInLane[0].x);

                for (int i = 0; i < GM.balls.Count; i++)
                {
                    GM.balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, GM.balls[i].GetComponent<Rigidbody>().velocity.y / 4, 0);
                }
                for (int i = 0; i < GM.lanetextscript.Length; i++)
                {
                    GM.lanetextscript[i].scrollSpeed /= 4;
                    lanerenders[i].sharedMaterial = slowMat;
                }
                GM.spawnRate *= 2f;
                SlowObj.SetActive(true);
                break;
            default:
                break;
        }
    }

    internal void disableSlow()
    {
        for (int i = 0; i < GM.balls.Count; i++)
        {
            GM.balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, GM.balls[i].GetComponent<Rigidbody>().velocity.y * 4, 0);
        }
        for (int i = 0; i < GM.lanetextscript.Length; i++)
        {
            GM.lanetextscript[i].scrollSpeed *= 4;
            lanerenders[i].sharedMaterial = defaultMat;
        }
        GM.spawnRate /= 2f;
        slowTime = Time.time + (slowCD*(1-((abilitysInLane[0].x -Time.time)/SlowDuration)));
        slowRemaning = Mathf.FloorToInt(slowTime);
        abb[0].isCD(true);
        abb[0].active = false;
        abb[0].setColor();
        abilitysInLane[0].y = 0;
        SlowObj.SetActive(false);
    }
}

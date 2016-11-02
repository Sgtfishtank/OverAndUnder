using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Abilitys : MonoBehaviour
{
    public MeshRenderer[] lanerenders;
    public Vector3[] abilitysInLane;
    public AbilityButton[] abb;
    public GameObject SlowObj;
    public GameObject WallObj;
    public GameObject MultiObj;
    public GameObject SwitchObj;
    
    public bool abilityActive;
    public float SlowDuration;
    public float WallDuration;
    public float MultiDuration;
    public float slowCD;
    public float wallCD;
    public float multiCD;
    public float switchCD;
    
    public AbilitysEnum currentActive;
    public bool selectingAbility = false;
    private Color defaultcolor;
    private Color slowcolor;
    private Color wallcolor;
    private Color multicolor;
    private Color switchcolor;
    public Color blue;
    public Color red;
    public Color purple;

    internal int slowRemaning;
    internal int wallRemaning;
    internal int switchRemaning;
    internal int multiRemaning;

    private GameMaster GM;
    private float slowTime;
    private float wallTime;
    private float multiTime;
    private float switchTime;

    public enum AbilitysEnum
    {
        SLOW, WALL, MULTIPLIER, SWITCH, NONE
    };
    void Start()
    {
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);
        ColorUtility.TryParseHtmlString("#121E3300", out slowcolor);
        ColorUtility.TryParseHtmlString("#330F0F00", out wallcolor);
        ColorUtility.TryParseHtmlString("#300B3300", out multicolor);
        ColorUtility.TryParseHtmlString("#33310E00", out switchcolor);
        ColorUtility.TryParseHtmlString("#00FAFFFF", out blue);
        ColorUtility.TryParseHtmlString("#FF8E00FF", out red);
        ColorUtility.TryParseHtmlString("#D841FBFF", out purple);

        SlowObj = Instantiate(SlowObj, Vector3.zero, Quaternion.identity) as GameObject;
        SlowObj.SetActive(false);
        WallObj = Instantiate(WallObj, Vector3.zero, Quaternion.identity) as GameObject;
        WallObj.SetActive(false);
        MultiObj = Instantiate(MultiObj, Vector3.zero, Quaternion.identity) as GameObject;
        MultiObj.SetActive(false);
        SwitchObj = Instantiate(SwitchObj, Vector3.zero, Quaternion.identity) as GameObject;
        SwitchObj.SetActive(false);

        GM = transform.GetComponent<GameMaster>();

        lanerenders = transform.GetComponentsInChildren<Transform>().Where(x => x.tag == "Lane").Select(x => x.transform.GetComponent<MeshRenderer>()).ToArray();
        initalize();
    }
    public void Reset()
    {
        initalize();
        slowTime = 0;
        wallTime = 0;
        multiTime = 0;
        switchTime = 0;
    }
    void initalize()
    {
        abb = transform.GetComponentsInChildren<AbilityButton>();
    }
    void Update()
    {
        for (int i = 0; i < abilitysInLane.Length; i++)
        {
            if (abilitysInLane[i].y == 1 && abilitysInLane[i].x < Time.time)
            {
                if (i == 0)
                {
                    slowReset((int)abilitysInLane[i].z);
                }
                else if (i == 1)
                {
                    wallReset((int)abilitysInLane[i].z);
                }
                else if (i == 2)
                {
                    multiReset((int)abilitysInLane[i].z);
                }
            }
        }

        if (slowTime < Time.time)
        {
            abb[0].isCD(false);
        }
        /*if (wallTime < Time.time)
        {
            abb[1].isCD(false);
        }
        if (multiTime < Time.time)
        {
            abb[2].isCD(false);
        }
        if (switchTime < Time.time)
        {
            abb[3].isCD(false);
        }
        if ((switchTime - Time.time - switchCD) < -0.7f)
        {
            SwitchObj.SetActive(false);
        }*/
    }
    void slowReset(int lane)
    {

        for (int i = 0; i < GM.balls.Count; i++)
        {
            if (GM.balls[i].activeSelf && GM.balls[i].transform.GetComponent<Ball>().lane == lane)
            {
                GM.balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, GM.balls[i].GetComponent<Rigidbody>().velocity.y * 2, 0);
            }
        }
        for (int i = 0; i < GM.lanetextscript.Length; i++)
        {
            GM.lanetextscript[i].scrollSpeed /= 2;
            GM.lanetextscript[i].scrollSpeed2 /= 2;
            lanerenders[i].material.SetColor("_EmissionColor", defaultcolor);
        }
        //GM.lanetextscript[lane].scrollSpeed *= 2;
        //GM.lanetextscript[lane].scrollSpeed2 *= 2;
        //lanerenders[lane].material.SetColor("_EmissionColor", defaultcolor);
        slowTime = Time.time + slowCD;
        slowRemaning = Mathf.FloorToInt(slowTime);
        abb[0].isCD(true);
        abb[0].active = false;
        abb[0].setColor();
        abilitysInLane[0].y = 0;
        SlowObj.SetActive(false);
    }
    void wallReset(int lane)
    {
        WallObj.SetActive(false);
        lanerenders[lane].material.SetColor("_EmissionColor", defaultcolor);
        abilitysInLane[1].y = 0;
        wallTime = Time.time + wallCD;
        wallRemaning = Mathf.FloorToInt(wallTime);
        abb[1].active = false;
        abb[1].isCD(true);
        abb[1].setColor();
    }
    void multiReset(int lane)
    {
        for (int i = 0; i < GM.balls.Count; i++)
        {
            if (GM.balls[i].activeSelf && GM.balls[i].transform.GetComponent<Ball>().lane == lane)
            {
                if (GM.balls[i].tag == "Red")
                    GM.balls[i].GetComponentInChildren<ParticleSystem>().startColor = red;
                else
                    GM.balls[i].GetComponentInChildren<ParticleSystem>().startColor = blue;
            }
        }
        MultiObj.SetActive(false);
        multiTime = Time.time + multiCD;
        multiRemaning = Mathf.FloorToInt(multiTime);
        lanerenders[lane].material.SetColor("_EmissionColor", defaultcolor);
        abilitysInLane[2].y = 0;
        abb[2].isCD(true);
        abb[2].active = false;
        abb[2].setColor();
    }
    public bool highlightLanes(AbilitysEnum ability)
    {
        if (selectingAbility)
            return false;
        currentActive = ability;
        selectingAbility = true;
        switch (currentActive)
        {
            case AbilitysEnum.SLOW:
                abb[0].active = true;
                abb[0].setColor();
                break;
            case AbilitysEnum.WALL:
                abb[1].active = true;
                abb[1].setColor();
                break;
            case AbilitysEnum.MULTIPLIER:
                abb[2].active = true;
                abb[2].setColor();
                break;
            case AbilitysEnum.SWITCH:
                abb[3].active = true;
                abb[3].setColor();
                break;
            default:
                break;
        }
        return true;
    }
    public void unlightLanes()
    {
        currentActive = AbilitysEnum.NONE;
        selectingAbility = false;
        switch (currentActive)
        {
            case AbilitysEnum.SLOW:
                abb[0].active = false;
                abb[0].setColor();
                break;
            case AbilitysEnum.WALL:
                abb[1].active = false;
                abb[1].setColor();
                break;
            case AbilitysEnum.MULTIPLIER:
                abb[2].active = false;
                abb[2].setColor();
                break;
            case AbilitysEnum.SWITCH:
                abb[3].active = false;
                abb[3].setColor();
                break;
            default:
                break;
        }

    }
    public void setLane(int lane)
    {
        if (selectingAbility && !GM.destroyedLanes.Contains(lane))
        {
            activateAbility(currentActive, lane);
        }
    }
public void activateAbility(AbilitysEnum a, int lane)
    {
        abilityActive = true;
        selectingAbility = false;
        switch (a)
        {
            case AbilitysEnum.SLOW:
                abilitysInLane[0].x = Time.time + SlowDuration;
                abilitysInLane[0].y = 1;
                abilitysInLane[0].z = lane;
                slowRemaning = Mathf.FloorToInt(abilitysInLane[0].x);

                for (int i = 0; i < GM.balls.Count; i++)
                {
                    /*if (GM.balls[i].activeSelf && GM.balls[i].transform.GetComponent<Ball>().lane == lane)
                    {*/
                        GM.balls[i].GetComponent<Rigidbody>().velocity = new Vector3(0, GM.balls[i].GetComponent<Rigidbody>().velocity.y / 2, 0);
                    //}
                }
                for (int i = 0; i < GM.lanetextscript.Length; i++)
                {
                    GM.lanetextscript[i].scrollSpeed /= 2;
                    GM.lanetextscript[i].scrollSpeed2 /= 2;
                    lanerenders[i].material.SetColor("_EmissionColor", slowcolor);
                }
                //GM.lanetextscript[lane].scrollSpeed /= 2;
                //GM.lanetextscript[lane].scrollSpeed2 /= 2;

                
                SlowObj.SetActive(true);
                /*
                switch (lane)
                {
                    case 0:
                        SlowObj.transform.position = new Vector3(-2.07f, 3, 0);
                        SlowObj.transform.rotation = Quaternion.identity;
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = -1;
                        break;
                    case 1:
                        SlowObj.transform.position = new Vector3(-0.64f, 3, 0);
                        SlowObj.transform.rotation = Quaternion.identity;
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = -1;
                        break;
                    case 2:
                        SlowObj.transform.position = new Vector3(0.79f, 3, 0);
                        SlowObj.transform.rotation = Quaternion.identity;
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = -1;
                        break;
                    case 3:
                        SlowObj.transform.position = new Vector3(-2f, -3, 0);
                        SlowObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = 1;
                        break;
                    case 4:
                        SlowObj.transform.position = new Vector3(-0.6f, -3, 0);
                        SlowObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = 1;
                        break;
                    case 5:
                        SlowObj.transform.position = new Vector3(0.85f, -3, 0);
                        SlowObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        SlowObj.transform.GetComponentInChildren<ParticleSystem>().gravityModifier = 1;
                        break;
                    default:
                        break;
                }*/

                break;
            case AbilitysEnum.WALL:
                abilitysInLane[1].x = Time.time + WallDuration;
                abilitysInLane[1].y = 1;
                abilitysInLane[1].z = lane;
                wallRemaning = Mathf.FloorToInt(abilitysInLane[1].x);


                switch (lane)
                {
                    case 0:
                        WallObj.transform.position = new Vector3(-1.44f, 0, 0);
                        WallObj.transform.rotation = Quaternion.identity;

                        break;
                    case 1:
                        WallObj.transform.position = new Vector3(0, 0, 0);
                        WallObj.transform.rotation = Quaternion.identity;
                        break;
                    case 2:
                        WallObj.transform.position = new Vector3(1.44f, 0, 0);
                        WallObj.transform.rotation = Quaternion.identity;
                        break;
                    case 3:
                        WallObj.transform.position = new Vector3(-2.66f, 0, 0);
                        WallObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    case 4:
                        WallObj.transform.position = new Vector3(-1.2f, 0, 0);
                        WallObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    case 5:
                        WallObj.transform.position = new Vector3(0.21f, 0, 0);
                        WallObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    default:
                        break;
                }
                WallObj.SetActive(true);
                lanerenders[lane].material.SetColor("_EmissionColor", wallcolor);
                break;
            case AbilitysEnum.MULTIPLIER:
                abilitysInLane[2].x = Time.time + MultiDuration;
                abilitysInLane[2].y = 1;
                abilitysInLane[2].z = lane;
                multiRemaning = Mathf.FloorToInt(abilitysInLane[2].x);
                for (int i = 0; i < GM.balls.Count; i++)
                {
                    if (GM.balls[i].activeSelf && GM.balls[i].transform.GetComponent<Ball>().lane == lane)
                    {
                        GM.balls[i].GetComponentInChildren<ParticleSystem>().startColor = purple;
                    }
                }
                switch (lane)
                {
                    case 0:
                        MultiObj.transform.position = new Vector3(-2.07f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.identity;

                        break;
                    case 1:
                        MultiObj.transform.position = new Vector3(-0.64f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.identity;
                        break;
                    case 2:
                        MultiObj.transform.position = new Vector3(0.79f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.identity;
                        break;
                    case 3:
                        MultiObj.transform.position = new Vector3(-2f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    case 4:
                        MultiObj.transform.position = new Vector3(-0.6f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    case 5:
                        MultiObj.transform.position = new Vector3(0.85f, 0, 0);
                        MultiObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        break;
                    default:
                        break;
                }
                MultiObj.SetActive(true);
                lanerenders[lane].material.SetColor("_EmissionColor", multicolor);
                break;
            case AbilitysEnum.SWITCH:
                GM.boxes[lane].transform.GetComponent<Box>().changeColor();
                switchTime = Time.time + switchCD;
                switchRemaning = Mathf.FloorToInt(switchTime);
                SwitchObj.transform.position = GM.boxPoints[lane].transform.position + new Vector3(0, 0, -2);
                SwitchObj.SetActive(true);
                abb[3].isCD(true);
                abb[3].active = false;
                abb[3].setColor();
                break;
            default:
                break;
        }

    }


}

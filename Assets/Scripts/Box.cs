﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Box : MonoBehaviour
{
    public new Camera camera;
    public List<Transform> BoxSlots;
    public int Slot;
    public GameMaster GM;

    public float speed;
    public int hp = 25;
    private bool move;
    private float healTimer;
    public float healCD;
    //private int moving;
    public GameObject psMaster;
    public GameObject fullBox;
    public GameObject brokenBox;
    public GameObject CrystalText;
    public Material mat1;
    public Material mat2;
    public Color blue;
    public Color red;
    public Color grey;
    public Color defaultColor;
    public Color scoreEffect;
    public Color scoreBlink;
    public Color black;
    public Color selectedcolor;
    private float shaketime;

    bool isBroken;
    private float explotionDur;
    private float crystalTextDur;
    private Abilitys AM;
    private bool once = true;
    Transform[] temp;
    public bool ghost;
    private bool selected;
    internal int currentMultiplier = 1;
    private int currentLevel;
    private int maxHp;
	private float scoreBlinktime;

    // Use this for initialization
    void Start ()
    {
        hp = GlobalVariables.Instance.HPLevel3;//ConfigReader.Instance.getValueInt("UpgradeHPLevel")+3;
        maxHp = hp;
        ColorUtility.TryParseHtmlString("#909090FF", out grey);
        ColorUtility.TryParseHtmlString("#00000000", out defaultColor);
        ColorUtility.TryParseHtmlString("#FFCD63FF", out scoreEffect);
        ColorUtility.TryParseHtmlString("#C3C3C376", out selectedcolor);
        ColorUtility.TryParseHtmlString("#00000076", out black);
        ColorUtility.TryParseHtmlString("#4A4A4A", out scoreBlink);
        camera = GameObject.Find("camera_main").GetComponent<Camera>();
        GM = GameObject.Find("Game Master(Clone)").GetComponent<GameMaster>();
        AM = GameObject.Find("Game Master(Clone)").GetComponent<Abilitys>();
        BoxSlots = GM.boxPoints.GetRange(0, GM.boxPoints.Count);
        currentLevel = GM.currentLevel;
        psMaster.SetActive(false);
        if (!ghost)
        {
            brokenBox.transform.gameObject.SetActive(false);
            temp = fullBox.transform.GetComponentsInChildren<Transform>();
        }
        if (currentLevel == 1 && BoxSlots.Count == 7)
            BoxSlots.RemoveAt(6);
    }
    

    // Update is called once per frame
    void Update ()
    {
        if (once && !ghost)
        {
        
            temp = fullBox.transform.GetComponentsInChildren<Transform>();
            
            for (int j = 0; j < temp.Length; j++)
            {
                temp[j].gameObject.layer = 8;
            }
            once = false;
        }

        if (!Input.GetMouseButton(0) && transform.position != BoxSlots[Slot].position /*&& moving == 0*/)
        {
            transform.position = Vector3.MoveTowards(transform.position, BoxSlots[Slot].position, speed * Time.deltaTime);
        }

        if (ghost)
            return;

        if (shaketime > Time.time)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Sin(Time.time *70)*2.7f));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        if(scoreBlinktime < Time.time)
        {
            if (isBroken)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", defaultColor);
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_EmissionColor", defaultColor);
            }
        }
        if (hp == 0)
        {
            GM.destroyLane(Slot);
            hp--;
            selected = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            brokenBox.GetComponent<MeshRenderer>().material.SetColor("_Color", grey);
        }
        if(crystalTextDur < Time.time)
        {
            CrystalText.SetActive(false);
        }
        if(explotionDur < Time.time)
        {
            psMaster.SetActive(false);
        }
        
        
        if (currentLevel != 1)
        {
            if (Slot == BoxSlots.Count - 1)
            {
                if (healTimer < Time.time && hp < maxHp)//ConfigReader.Instance.getValueInt("UpgradeHPLevel") +3)
                {
                    hp++;
                    //ConfigReader.Instance.changeValue("Healed", ConfigReader.Instance.getValueInt("Healed")+1);
                    healTimer = Time.time + healCD;
					if (isBroken && hp > maxHp / 2)
					{
						isBroken = false;
						brokenBox.transform.gameObject.SetActive(false);
						fullBox.transform.gameObject.SetActive(true);
					}
				}
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        CrystalText.GetComponentsInChildren<TextMesh>(true)[0].text = "+" +1 * currentMultiplier;
        if(col.transform.CompareTag("Red") &&  transform.CompareTag("Red Box"))
        {
            if(Slot> 2)
                CrystalText.transform.position = col.transform.position - new Vector3(0,0.2f,0);
            else
                CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addRedScore(1 * currentMultiplier);
            if (isBroken)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", scoreBlink);
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_EmissionColor", scoreBlink);
            }
            scoreBlinktime = Time.time + 0.1f;
        }
        else if (col.transform.CompareTag("Blue") && transform.CompareTag("Blue Box"))
        {
            if (Slot > 2)
                CrystalText.transform.position = col.transform.position - new Vector3(0, 0.2f, 0);
            else
                CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addBlueScore(1 * currentMultiplier);
            if (isBroken)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", scoreBlink);
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_EmissionColor", scoreBlink);
            }
            scoreBlinktime = Time.time + 0.1f;
        }
        else
        {
            psMaster.SetActive(true);
            psMaster.transform.position = col.transform.position;
            explotionDur = Time.time + 1f;
            shaketime = 0.3f + Time.time;
            GM.resetScoreStreak();
            //ConfigReader.Instance.changeValue("ShieldLost", ConfigReader.Instance.getValueInt("ShieldLost")+1);
            hp--;
			if (!isBroken && hp < maxHp / 2)
			{
				isBroken = true;
				brokenBox.SetActive(true);
				fullBox.SetActive(false);
			}
		}
        col.gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GM.selectBox(Slot);
        }
    }
    public void NewPos(int pos)
    {
        Slot = pos;
    }
    public void StartPos(int pos)
    {
        Slot = pos;
    }
    public void TakeDamage()
    {
        if (hp <= 0)
            return;
        hp--;
        shaketime = 0.3f + Time.time;
    }
    public void ScoreStreakOn()
    {
        if (transform.CompareTag("Ghost"))
            return;
        if (isBroken)
        {
            brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_EmissionColor", scoreEffect);
            brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_Color", scoreEffect);
        }
        else
        {
            fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", scoreEffect);
            fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_Color", scoreEffect);
        }
	}
    public void ScoreStreakOff()
    {
        if (transform.CompareTag("Ghost") || hp <= 0)
            return;
        if (isBroken)
        {
            brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_EmissionColor", defaultColor);
            if(transform.CompareTag("Red Box"))
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_Color", red);
            else
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_Color", blue);
        }
        else
        {
            fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", defaultColor);
            if(transform.CompareTag("Red Box"))
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_Color", red);
            else
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_Color", blue);
        }
    }
    public void Select(bool _selected)
    {
        if(_selected)
        {
            if (isBroken)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_Color", selectedcolor);//black
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", selectedcolor);//nr 4
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[2].SetColor("_Color", selectedcolor);//nr 4
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", selectedcolor);//black
            }
        }
        else
        {
            if (isBroken)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_Color", black);//black
                //brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", scoreEffect);//nr 4
                if (transform.CompareTag("Red Box"))
                    fullBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", red);
                else
                    fullBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", blue);
            }
            else
            {
                //fullBox.GetComponentInChildren<MeshRenderer>(true).materials[2].SetColor("_Color", scoreEffect);//nr 4
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[4].SetColor("_Color", black);//black
                if (transform.CompareTag("Red Box"))
                    fullBox.GetComponentInChildren<MeshRenderer>(true).materials[2].SetColor("_Color", red);
                else
                    fullBox.GetComponentInChildren<MeshRenderer>(true).materials[2].SetColor("_Color", blue);
            }
        }
    }
}

using UnityEngine;
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
    private bool move = false;
    private float healTimer;
    public float healCD;
    private int moving;
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
    private float shaketime;

    bool isBroken = false;
    private float explotionDur;
    private float crystalTextDur;
    private Abilitys AM;
    private bool once = true;
    Transform[] temp;
    public bool ghost = false;
    private bool movable = true;
    private bool selected = false;
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
        ColorUtility.TryParseHtmlString("#4A4A4A", out scoreBlink);
        camera = GameObject.Find("camera_main").GetComponent<Camera>();
        GM = GameObject.Find("Game Master(Clone)").GetComponent<GameMaster>();
        AM = GameObject.Find("Game Master(Clone)").GetComponent<Abilitys>();
        BoxSlots = GM.boxPoints.GetRange(0, GM.boxPoints.Count);
        currentLevel = GM.currentLevel;
        psMaster.SetActive(false);
        brokenBox.transform.gameObject.SetActive(false);
        temp = fullBox.transform.GetComponentsInChildren<Transform>();
        if (currentLevel == 1 && BoxSlots.Count == 7)
            BoxSlots.RemoveAt(6);
    }
    

    // Update is called once per frame
    void Update ()
    {
        if (moving == 2)
        {
            for (int i = 0; i < BoxSlots.Count; i++)
            {
                if (Mathf.Abs((transform.position.x - BoxSlots[i].position.x)) <= 0.75f && Mathf.Abs((transform.position.y - BoxSlots[i].position.y)) <= 0.75f)
                {
                    move = true;
                    GM.swapBox(Slot, i);
                    Slot = i;
                    moving = 0;
                    if (ghost)
                    {
                        /*if (i == 6)
                        {
                            fullBox.transform.transform.localScale = new Vector3(0.7743875f, 0.7743875f, 0.7743875f);
                            brokenBox.transform.transform.localScale = new Vector3(0.7743875f, 0.7743875f, 0.7743875f);
                        }
                        else
                        {
                            fullBox.transform.transform.localScale = new Vector3(1, 1, 1);
                            brokenBox.transform.transform.localScale = new Vector3(1,1, 1);
                        }*/
                    }
                }
            }
        }
        if (once)
        {
            temp = fullBox.transform.GetComponentsInChildren<Transform>();
            
            for (int j = 0; j < temp.Length; j++)
            {
                temp[j].gameObject.layer = 8;
            }
            once = false;
        }
        if(ghost)
        {
            return;
        }
        if(selected)
        {
            Vector3 pos = Input.mousePosition;
            transform.position = camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, camera.nearClipPlane));
            transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
        }
        if(shaketime > Time.time)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Sin(Time.time *70)*2.7f));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        if(scoreBlinktime < Time.time)
        {
            if (hp < maxHp / 2)
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
            GM.unLockBoxes(Slot);
            movable = false;
            selected = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            brokenBox.GetComponent<MeshRenderer>().material.SetColor("_Color", grey);
        }
        if(!isBroken && hp < maxHp/2)
        {
            isBroken = true;
            brokenBox.SetActive(true);
            fullBox.SetActive(false);
        }
        else if(isBroken &&hp > maxHp/2)
        {
            isBroken = false;
            brokenBox.transform.gameObject.SetActive(false);
            fullBox.transform.gameObject.SetActive(true);
        }
        if(crystalTextDur < Time.time)
        {
            CrystalText.SetActive(false);
        }
        if(explotionDur < Time.time)
        {
            psMaster.SetActive(false);
        }
        
        if (!Input.GetMouseButton(0) && transform.position != BoxSlots[Slot].position && moving == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, BoxSlots[Slot].position, speed * Time.deltaTime);
                
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
                }
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        CrystalText.GetComponentsInChildren<TextMesh>(true)[0].text = "+" +1 * currentMultiplier;
        if(col.transform.tag == "Red" &&  transform.tag == "Red Box")
        {
            if(Slot> 2)
                CrystalText.transform.position = col.transform.position - new Vector3(0,0.2f,0);
            else
                CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addRedScore(1 * currentMultiplier);
            if (hp < maxHp / 2)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", scoreBlink);
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_EmissionColor", scoreBlink);
            }
            scoreBlinktime = Time.time + 0.2f;
        }
        else if (col.transform.tag == "Blue" && transform.tag == "Blue Box")
        {
            if (Slot > 2)
                CrystalText.transform.position = col.transform.position - new Vector3(0, 0.2f, 0);
            else
                CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addBlueScore(1 * currentMultiplier);
            if (hp < maxHp / 2)
            {
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", scoreBlink);
            }
            else
            {
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[1].SetColor("_EmissionColor", scoreBlink);
            }
            scoreBlinktime = Time.time + 0.2f;
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
        }
        col.gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (movable)
            {
                selected = true; 
                Vector3 pos = Input.mousePosition;
                transform.position = camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, camera.nearClipPlane));
                transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
            }
            GM.LockBoxes(Slot);
            if (AM.selectingAbility && AM.currentActive == Abilitys.AbilitysEnum.SWITCH)
            {
                AM.activateAbility(Abilitys.AbilitysEnum.SWITCH, Slot);
            }

        }
        if(!Input.GetMouseButton(0))
        {
            move = false;
            
            GM.unLockBoxes(Slot);
            for (int i = 0; i < BoxSlots.Count; i++)
            {
                if (!selected)
                    return;
                if (i < 3)
                {
                    if (Mathf.Abs((transform.position.x - BoxSlots[i].position.x)) <= 0.75f && Mathf.Abs((transform.position.y - BoxSlots[i].position.y-5)) <= 5.75f)
                    {
                        if (!GM.destroyedLanes.Contains(i))
                        {
                            move = true;
                            GM.swapBox(Slot, i);
                            Slot = i;
                            if (Slot == 6)
                            {
                                transform.transform.localScale = new Vector3(0.7743875f, 0.7743875f, 0.7743875f);
                            }
                            else
                            {
                                transform.transform.localScale = new Vector3(1, 1, 1);
                            }
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs((transform.position.x - BoxSlots[i].position.x)) <= 0.75f && Mathf.Abs((transform.position.y - BoxSlots[i].position.y+5)) <= 5.75f)
                    {
                        if (!GM.destroyedLanes.Contains(i))
                        {
                            move = true;
                            GM.swapBox(Slot, i);
                            Slot = i;
                            if (Slot == 6)
                            {
                                transform.transform.localScale = new Vector3(0.7743875f, 0.7743875f, 0.7743875f);
                            }
                            else
                            {
                                transform.transform.localScale = new Vector3(1, 1, 1);
                            }
                        }
                    }
                }
            }
            selected = false;
        }
    }
    public void newPos(int pos)
    {
        Slot = pos;
    }
    public void StartPos(int pos)
    {
        Slot = pos;
    }
    public void onTheMove(int a)
    {
        moving = a;
    }
    public void changeColor()
    {
        if (transform.tag == "Red Box")
        {
            transform.tag = "Blue Box";
            transform.GetComponentsInChildren<MeshRenderer>(true)[0].sharedMaterial = mat2;
            transform.GetComponentsInChildren<MeshRenderer>(true)[2].sharedMaterial = mat2;
            transform.GetComponentInChildren<TextMesh>(true).color = blue;
        }
        else
        {
            transform.tag = "Red Box";
            transform.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial = mat1;
            transform.GetComponentsInChildren<Renderer>(true)[2].sharedMaterial = mat1;
            transform.GetComponentInChildren<TextMesh>(true).color = red;
        }
    }
    public void canMove()
    {
        movable = true;
    }
    public void cantMove()
    {
        movable = false;
    }
    public void takeDamage()
    {
        hp--;
        shaketime = 0.3f + Time.time;
    }
    public void scoreStreakOn()
    {
        if (hp < maxHp / 2)
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
    public void scoreStreakOff()
    {
        if (hp < maxHp / 2)
        {
            brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_EmissionColor", defaultColor);
            if(transform.tag == "Red Box")
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_Color", red);
            else
                brokenBox.GetComponentInChildren<MeshRenderer>(true).materials[0].SetColor("_Color", blue);
        }
        else
        {
            fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_EmissionColor", defaultColor);
            if(transform.tag == "Red Box")
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_Color", red);
            else
                fullBox.GetComponentInChildren<MeshRenderer>(true).materials[3].SetColor("_Color", blue);
        }
    }
}

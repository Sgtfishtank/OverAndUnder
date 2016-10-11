using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Box : MonoBehaviour
{
    public new Camera camera;
    public Transform[] BoxSlots;
    public int Slot;
    public GameMaster GM;
    public float speed;
    public int hp = 25;
    private bool move = false;
    private float healTimer;
    public float healCD;
    private int moving;
    public ParticleSystem ps;
    public GameObject psMaster;
    public GameObject fullBox;
    public GameObject brokenBox;
    public GameObject CrystalText;
    bool isBroken = false;
    private float explotionDur;
    private float crystalTextDur;

    // Use this for initialization
    void Start ()
    {
        camera = GameObject.Find("camera_main").GetComponent<Camera>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        BoxSlots = GM.boxPoints.ToArray();
        //ps = transform.GetComponentInChildren<ParticleSystem>();
        //psMaster = transform.GetComponentsInChildren<GameObject>().Where(x => x.name == "particles_explosion").Select(x => x.transform).ToArray()[0].gameObject;

        brokenBox.SetActive(false);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(hp <= 0)
        {
            GM.destroyLane(Slot);
            gameObject.SetActive(false);
        }
        if(!isBroken && hp < 6)
        {
            isBroken = true;
            brokenBox.SetActive(true);
            fullBox.SetActive(false);
        }
        else if(isBroken &&hp > 5)
        {
            isBroken = false;
            brokenBox.SetActive(false);
            fullBox.SetActive(true);
        }
        if(crystalTextDur < Time.time)
        {
            CrystalText.SetActive(false);
        }
        if(explotionDur < Time.time)
        {
            psMaster.SetActive(false);
        }
        if (moving == 2)
        {
            for (int i = 0; i < BoxSlots.Length; i++)
            {
                if (Mathf.Abs((transform.position.x - BoxSlots[i].position.x)) <= 0.75f && Mathf.Abs((transform.position.y - BoxSlots[i].position.y)) <= 0.75f)
                {
                    move = true;
                    GM.swapBox(Slot, i);
                    Slot = i;
                    moving = 0;
                }
            }
        }
        if (!Input.GetMouseButton(0) && transform.position != BoxSlots[Slot].position && moving == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, BoxSlots[Slot].position, speed * Time.deltaTime);
                
        }
        if(Slot == BoxSlots.Length-1)
        {
            if(healTimer < Time.time && hp < 26)
            {
                hp++;
                healTimer = Time.time + healCD;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(1 == GM.abilitysInLane[Slot+12].y)
        {
            CrystalText.GetComponentsInChildren<TextMesh>(true)[0].text = "+3";
        }
        else
        {
            CrystalText.GetComponentsInChildren<TextMesh>(true)[0].text = "+1";
        }
        if(col.transform.tag == "Red" &&  transform.tag == "Red Box")
        {
            if(Slot> 2)
                CrystalText.transform.position = col.transform.position - new Vector3(0,0.2f,0);
            else
                CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addScore(Slot);
        }
        else if (col.transform.tag == "Blue" && transform.tag == "Blue Box")
        {
            CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addScore(Slot);
        }
        else if (col.transform.tag == "Gold")
        {
            CrystalText.transform.position = col.transform.position;
            crystalTextDur = Time.time + 1;
            CrystalText.SetActive(true);
            GM.addScore(Slot);
        }
        else
        {
            psMaster.SetActive(true);
            psMaster.transform.position = col.transform.position;
            explotionDur = Time.time + 2f;
            hp--;
        }
        col.gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            transform.position = camera.ScreenToWorldPoint(new Vector3(pos.x,pos.y,camera.nearClipPlane));
            transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
           
        }
        if(!Input.GetMouseButton(0))
        {
            move = false;
            for (int i = 0; i < BoxSlots.Length; i++)
            {
                if (Mathf.Abs((transform.position.x - BoxSlots[i].position.x)) <= 0.75f && Mathf.Abs((transform.position.y - BoxSlots[i].position.y)) <= 0.75f)
                {
                    if (!GM.destroyedLanes.Contains(i))
                    {
                        move = true;
                        GM.swapBox(Slot, i);
                        Slot = i;
                    }
                    //transform.position = BoxSlots[i].position;
                }
            }
            if(!move)
            {
                //transform.position = BoxSlots[Slot].position;
            }
        }
    }
    public void newPos(int pos)
    {
        Slot = pos;
        //transform.position = BoxSlots[Slot].position;
    }
    public void StartPos(int pos)
    {
        Slot = pos;
    }
    public void onTheMove(int a)
    {
        moving = a;
    }
}

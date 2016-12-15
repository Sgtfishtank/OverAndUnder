using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {
    GameMaster GM;
    public Box[] boxesscripts;
    public GameObject psMaster;
    private float explotionDur;
    private int currentLevel;
    // Use this for initialization
    void Start ()
    {
        GM =GameObject.Find("Game Master(Clone)").GetComponent<GameMaster>();
        GameObject[] boxes =GM.boxes.ToArray();

        if (boxes.Length == 7)
        {
            for (int i = 0; i < boxes.Length-1; i++)
            {
                boxesscripts[i] = boxes[i].GetComponent<Box>();
            }
        }
        else
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxesscripts[i] = boxes[i].GetComponent<Box>();
            }
        }
        psMaster.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (explotionDur < Time.time)
        {
            psMaster.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(currentLevel < 4)
        {
            boxesscripts[2].takeDamage();
            boxesscripts[5].takeDamage();
        }
        if(currentLevel < 10)
        {
            boxesscripts[1].takeDamage();
            boxesscripts[4].takeDamage();
        }
        if(currentLevel > 9)
        {
            boxesscripts[0].takeDamage();
            boxesscripts[3].takeDamage();
        }
        col.gameObject.SetActive(false);
        psMaster.SetActive(true);
        psMaster.transform.position = col.transform.position;
        explotionDur = Time.time + 1f;
        ConfigReader.Instance.changeValue("HeartHits", ConfigReader.Instance.getValueInt("HeartHits"));
    }
    public void setLevel(int value)
    {
        currentLevel = value;
    }
}   
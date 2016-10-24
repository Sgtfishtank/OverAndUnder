using UnityEngine;
using System.Collections;

public class LaneClick : MonoBehaviour
{
    public Abilitys GM;
    public int lane;
    
    void Start()
    {
        GM = GameObject.Find("Game Master").GetComponent<Abilitys>();
        
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GM.setLane(lane);
        }
    }
}

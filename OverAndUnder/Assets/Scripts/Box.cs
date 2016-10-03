using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour
{
    public Camera camera;
    public List<GameObject> BoxSlots;
    public int Slot;
    public GameMaster GM;
    // Use this for initialization
    void Start ()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            transform.position = camera.ScreenToWorldPoint(new Vector3(pos.x,pos.y,camera.nearClipPlane));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        if(Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < BoxSlots.Count; i++)
            {
                if (Mathf.Abs((transform.position - BoxSlots[i].transform.position).magnitude) <= 0.75f)
                {
                    GM.swapBox(Slot, i);
                    Slot = i;
                    transform.position = BoxSlots[i].transform.position;
                }
            }
            
        }
    }
    public void newPos(int pos)
    {
        Slot = pos;
        transform.position = BoxSlots[Slot].transform.position;
    }

}

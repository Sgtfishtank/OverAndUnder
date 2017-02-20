using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float Speed;
    public int x;
    public int y;
    public int z;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(x,y,z), Speed*Time.deltaTime);
	}
}

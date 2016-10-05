using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public int lane;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += new Vector3(Mathf.Sin(Time.time*25)*0.15f, 0, 0);
	}
}

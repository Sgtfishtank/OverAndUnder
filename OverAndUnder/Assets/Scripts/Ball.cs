using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time*30)*0.15f, transform.position.y, 0);
	}
}

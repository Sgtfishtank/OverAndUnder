using UnityEngine;
using System.Collections;

public class arrowEffectSqript : MonoBehaviour {
    float orgScale;
    public bool Gameover;
    // Use this for initialization
    void Start () {
        orgScale = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        if(Gameover)
            transform.localScale = new Vector3(transform.localScale.x, orgScale+Mathf.Sin(Time.time *5) * 0.01f, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x, orgScale + Mathf.Sin(Time.time * 5) * 0.001f, transform.localScale.z);
    }
}

using UnityEngine;
using System.Collections;

public class arrowEffectSqript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, 0.01721507f+Mathf.Sin(Time.time *2) * 0.001f, transform.localScale.z);
    }
}

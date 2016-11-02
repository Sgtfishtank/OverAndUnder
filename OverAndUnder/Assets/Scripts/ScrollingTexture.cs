using UnityEngine;
using System.Collections;

public class ScrollingTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ren = transform.GetComponent<Renderer>();

    }
    public float scrollSpeed = 0.90f;
    Renderer ren;

    void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;
        ren.material.mainTextureOffset = new Vector2(0, -offset);
    }
    // Update is called once per frame
    void Update () {
	
	}
}

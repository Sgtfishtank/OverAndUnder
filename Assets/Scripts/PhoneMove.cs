using UnityEngine;
using System.Collections;

public class PhoneMove : MonoBehaviour
{
    private float maxPickingDistance = 10000;// increase if needed, depending on your scene size

    private Transform pickedObject = null;
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        foreach (Touch touch in Input.touches)
        {
            //Create horizontal plane
            Plane horPlane = new Plane(Vector3.up, Vector3.zero);

            //Gets the ray at position where the screen is touched
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, maxPickingDistance))
                {
                    if (hit.transform.tag == "Blue Box" || hit.transform.tag == "Red Box")
                    {
                        if(hit.transform == pickedObject)
                        {
                            pickedObject.GetComponent<Box>().Select(false);
                        }
                        if(hit.transform != pickedObject)
                        {
                            int temp = pickedObject.GetComponent<Box>().Slot;
                            pickedObject.GetComponent<Box>().newPos(hit.transform.GetComponent<Box>().Slot);
                            hit.transform.GetComponent<Box>().newPos(temp);
                        }
                        if (pickedObject == null)
                        {
                            pickedObject = hit.transform;
                            pickedObject.GetComponent<Box>().Select(true);
                        }
                    }
                }
                else
                {
                    pickedObject = null;
                }
            }
           /* else if (touch.phase == TouchPhase.Moved)
            {
                if (pickedObject != null)
                {
                    float distance1 = 0f;
                    if (horPlane.Raycast(ray, out distance1))
                    {
                        pickedObject.transform.position = ray.GetPoint(distance1) + new Vector3(0, 0, 1);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                pickedObject.GetComponent<Box>().onTheMove(2);
                pickedObject = null;
            }*/
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBackGround : MonoBehaviour
{
    GameObject Cam;

	void Start ()
    {
		Cam = FindObjectOfType<SharedCamera>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y,0);
	}
}

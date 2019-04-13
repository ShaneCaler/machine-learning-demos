using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground3D : MonoBehaviour
{

	private BoxCollider gCollider;
	private float gHorizontalLen;

    // Start is called before the first frame update
    void Start()
    {
		gCollider = GetComponent<BoxCollider>();
		gHorizontalLen = gCollider.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(transform.position.x < -gHorizontalLen)
		{
			RepositionBackground();
		}*/
    }

	private void OnBecameInvisible()
	{
		RepositionBackground();
		Debug.Log("Reposition BG called");
	}

	private void RepositionBackground()
	{
		Vector3 gOffset = new Vector3(0f, 0f, gHorizontalLen * 2f);
		transform.position = transform.position + gOffset;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

	private BoxCollider2D gCollider;
	private float gHorizontalLen;

    // Start is called before the first frame update
    void Start()
    {
		gCollider = GetComponent<BoxCollider2D>();
		gHorizontalLen = gCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -gHorizontalLen)
		{
			RepositionBackground();
		}
    }

	private void RepositionBackground()
	{
		Vector2 gOffset = new Vector2(gHorizontalLen * 2, 0);
		transform.position = (Vector2)transform.position + gOffset;
	}
}

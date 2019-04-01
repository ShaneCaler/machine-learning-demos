using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	private BirdAcademy academy;
	private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
		ResetScrollingObject();
    }

	public void ResetScrollingObject()
	{
		academy = FindObjectOfType<BirdAcademy>();
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(academy.worldScrollSpeed, 0);
	}

    // Update is called once per frame
    void Update()
    {

    }
}

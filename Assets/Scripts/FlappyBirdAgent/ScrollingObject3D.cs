using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject3D : MonoBehaviour
{
	private RunnerAcademy academy;
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		ResetScrollingObject();
    }

	public void ResetScrollingObject()
	{
		academy = FindObjectOfType<RunnerAcademy>();
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0f, 0f, -academy.worldScrollSpeed);
	}

    // Update is called once per frame
    void Update()
    {

    }
}

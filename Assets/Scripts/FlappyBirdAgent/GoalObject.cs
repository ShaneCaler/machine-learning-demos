using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : MonoBehaviour
{
	private BirdAcademy academy;
	private bool collided = false;

	private void Start()
	{
		academy = FindObjectOfType<BirdAcademy>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("hit trigger " + collision.gameObject.name);
		if (collision.GetComponent<BirdAgent>() != null && !collided)
		{
			collided = true;
			Debug.Log("Score!!");
			academy.Score();
			StartCoroutine(TimeOut());
		}
	}

	IEnumerator TimeOut()
	{
		yield return new WaitForSeconds(2f);
		collided = false;
	}
}

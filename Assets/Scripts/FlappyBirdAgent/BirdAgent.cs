using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BirdAgent : Agent
{
	public float jumpForce = 200f;
	public bool isDead = false;
	public BirdAcademy academy;

	private Rigidbody2D rb;
	private Animator anim;
	private BirdRayPerception rayPer;
	private GoalObject[] goalObjects;
	private GoalObject nearestGoalObject;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		rayPer = GetComponent<BirdRayPerception>();
    }

    // Update is called once per frame
    void Update()
    {

    }

	public override void AgentReset()
	{
		academy.AcademyReset();
	}

	public override void CollectObservations()
	{
		float rayDistance = 100f;
		string[] detectableObjects = { "Column", "goal", "Obstacle" };

		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -1.5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -1f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -.5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right, detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, .5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, 1f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, 1.5f), detectableObjects));
		//AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position, Vector2.up, detectableObjects));
		//AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position, Vector2.down, detectableObjects));
		//AddVectorObs(rayPer.Perceive2(rayDistance, (Vector2)transform.position + new Vector2(.5f, 0f), Vector2.right + new Vector2(0f, -1f), detectableObjects));
		//AddVectorObs(rayPer.Perceive2(rayDistance, (Vector2)transform.position + new Vector2(.5f, 0f), Vector2.right + new Vector2(0f, 0f), detectableObjects));
		//AddVectorObs(rayPer.Perceive2(rayDistance, (Vector2)transform.position + new Vector2(.5f, 0f), Vector2.right + new Vector2(0f, 1f), detectableObjects));
		//AddVectorObs(rb.velocity.y);
		AddVectorObs(transform.position.y);

		goalObjects = academy.GetGoalObjects();
		if(goalObjects != null)
		{
			nearestGoalObject = GetNearestGoalObject(goalObjects);
			//Debug.Log("Nearest Goal Object: " + nearestGoalObject.name + " + position: " + nearestGoalObject.transform.position);
			float distance = Vector2.Distance(nearestGoalObject.transform.position, transform.position);
			//Debug.Log("Distance: " + distance);
			//AddVectorObs(distance);

			AddVectorObs(transform.position.x - nearestGoalObject.transform.position.x);
			AddVectorObs(transform.position.y - nearestGoalObject.transform.position.y);
			//AddVectorObs(nearestGoalObject.transform.position.y);

			Monitor.Log("V2Distance", distance.ToString("n6"));
			Monitor.Log("distance X", (transform.position.x - nearestGoalObject.transform.position.x).ToString("n6"));
			Monitor.Log("distance Y", (transform.position.y - nearestGoalObject.transform.position.y).ToString("n6"));

		}
		else
		{
			AddVectorObs(0f);
			AddVectorObs(0f);
			//AddVectorObs(0f);
		}

	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		Monitor.Log("VAct0", vectorAction[0].ToString("n6"));
		if (vectorAction[0] == 1)
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(0, jumpForce));
			anim.SetTrigger("Flap");
		}

		//AddReward(.05f);
		AddReward(-Mathf.Abs(transform.position.x - nearestGoalObject.transform.position.x) * Mathf.Abs(transform.position.y - nearestGoalObject.transform.position.y) * .01f);
		Monitor.Log("Current Reward", (-Mathf.Abs(transform.position.x - nearestGoalObject.transform.position.x) * Mathf.Abs(transform.position.y - nearestGoalObject.transform.position.y) * .01f).ToString("n6"));
	}

	private GoalObject GetNearestGoalObject(GoalObject[] goalObjs)
	{
		GoalObject nearestGoalObject = goalObjs[0];
		float dist, prevDist = 0;
		foreach(GoalObject goalObject in goalObjs)
		{
			dist = Vector2.Distance(goalObject.transform.position, transform.position);
			if (dist < prevDist)
				nearestGoalObject = goalObject;
			prevDist = dist;
		}
		return nearestGoalObject;
	}

	private void OnCollisionEnter2D()
	{
		//isDead = true;
		Debug.Log("collided");
		//anim.SetTrigger("Die");
		Done();
		SetReward(-1f);
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Obstacle"))
		{
			Debug.Log("Hit ceiling");
			AddReward(-.5f);
			Done();
		}
	} */
}

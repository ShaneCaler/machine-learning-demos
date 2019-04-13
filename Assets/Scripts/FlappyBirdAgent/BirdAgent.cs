using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BirdAgent : Agent
{
	public float jumpForce = 200f;
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
		float rayDistance = 75f;
		string[] detectableObjects = { "Column", "goal", "Obstacle" };
		string[] upDownObjects = { "Column" };

		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -1.5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -1f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, -.5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right, detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, .5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, 1f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.right + new Vector2(0f, 1.5f), detectableObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.up, upDownObjects));
		AddVectorObs(rayPer.Perceive(rayDistance, (Vector2)transform.position + new Vector2(.25f, 0f), Vector2.down, upDownObjects));
		//Vector2 normalizedVelocity = rb.velocity.normalized;
		AddVectorObs(rb.velocity.y / 10f);
		//Vector2 normalizedPosition = transform.position.normalized;
		AddVectorObs((Vector2)transform.localPosition);

		goalObjects = academy.GetGoalObjects();
		/* if(goalObjects != null)
		{
			nearestGoalObject = GetNearestGoalObject(goalObjects);
			AddVectorObs((Vector2)nearestGoalObject.transform.position.normalized);
			//AddVectorObs(nearestGoalObject.transform.position.x - transform.position.x);
			//AddVectorObs(nearestGoalObject.transform.position.y - transform.position.y);
			//Vector2 normalizedGoalObjectPosition = nearestGoalObject.transform.position.normalized;
			//AddVectorObs(nearestGoalObject.transform.position.y);
			//AddVectorObs(nearestGoalObject.transform.position.x);

			Monitor.Log("Nearest goal obj", nearestGoalObject.transform.position.normalized.ToString("n6"));
			//Monitor.Log("distance Y", (nearestGoalObject.transform.position.y - transform.position.y).ToString("n6"));
			//Monitor.Log("Goal Pos X", nearestGoalObject.transform.localPosition.x.ToString("n6"));
			//Monitor.Log("Goal Pos Y", nearestGoalObject.transform.localPosition.y.ToString("n6"));
			Monitor.Log("Inverse Vel", transform.InverseTransformDirection(rb.velocity).ToString());
		}
		else
		{
			AddVectorObs(Vector2.zero);
			//AddVectorObs(0f);
			//AddVectorObs(0f);
			//AddVectorObs(0f);
		} */

		Monitor.Log("Vel Y", (rb.velocity.y / 10f).ToString("n6"));
		Monitor.Log("Pos Norm", transform.localPosition.ToString("n6"));
		//Monitor.Log("Local Pos Y", transform.localPosition.y.ToString("n6"));
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

		AddReward(.01f);

		/* Vector2 forward = transform.TransformDirection(Vector2.right);
		Vector2 toOther = nearestGoalObject.transform.position - transform.position;
		if(Vector2.Dot(forward, toOther) > 0){
			float reward = Mathf.Abs((transform.position.x - nearestGoalObject.transform.position.x) * (transform.position.y - nearestGoalObject.transform.position.y)) * .01f;
			//Debug.Log("nearestGoalObject is in front of agent, adding reward of"
				//+ reward);
			AddReward(reward);
			Monitor.Log("Reward", reward.ToString("n6"));
		}
		else
		{
			Debug.Log("nearestGoalObject is behind agent");
		} */
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
		Debug.Log("collided");
		//anim.SetTrigger("Die");
		SetReward(-1f);
		Done();
	}

}

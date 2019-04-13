using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MLAgents;

// mlagents-learn config\trainer_config.yaml --run-id=snaketest3 --train
// tensorboard --logdir=summaries
// https://noobtuts.com/unity/2d-snake-game
public class SnakeAgent : Agent
{
	[SerializeField] private float moveSpeedMax = 1f;
	[SerializeField] private GameObject bodyPrefab;

	private List<Transform> bodyTransforms = new List<Transform>();
	private SnakeRayPerception rayPer;
	private SnakeAcademy academy;
	private Rigidbody2D rb;
	private Vector2 gridMoveDir = new Vector2();
	private Vector2 prevHeadPos = new Vector2();
	private bool hasEaten = false;
	private float moveSpeedTimer = 0f;
	private float rewardMultiplier = .01f;
	private int score = 0;
	private int maxScore = 0;

	void Start()
    {
		academy = FindObjectOfType<SnakeAcademy>();
		rayPer = GetComponent<SnakeRayPerception>();
		rb = GetComponent<Rigidbody2D>();
		moveSpeedTimer = moveSpeedMax;
    }

    void Update()
    {
		moveSpeedTimer += Time.deltaTime;
		if(moveSpeedTimer >= moveSpeedMax)
		{
			moveSpeedTimer -= moveSpeedMax;
			// Experimental - attempting to train the agent to make on-demand
			// decisions and actions based on a timer.
			RequestDecision(); 
			RequestAction(); // set the direction
			MoveAgent(); // move agent with previously set direction
		}
		
		if (score > maxScore)
		{
			maxScore = score;
			Debug.Log("New max score of " + maxScore);
		}
		Monitor.Log("Max Score", maxScore.ToString("n6"));
    }

	public override void AgentReset()
	{
		Debug.Log("Reset called");
		transform.position = new Vector2(0f, 0f);
		rewardMultiplier = .01f;
		score = 0;
		hasEaten = false;

		for(int i = 0; i < bodyTransforms.Count; i++)
		{
			Destroy(bodyTransforms[i].gameObject);
		}
		bodyTransforms.Clear();
	}

	public override void CollectObservations()
	{
		float rayDistance = 30f;
		string[] detectableObjects = { "Apple", "Body", "wall" };
		float[] rayAngles = { 90f, -90f, 180f, -180f, 45f, -45f, 135f, -135f, 0f };
		
		// Perceive returns a list of the distances to hit objects/rayDistance
		AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects));

		// TestPerceive attempts to get a list of x and y values of the hit objects
		AddVectorObs(rayPer.TestPerceive(rayDistance, rayAngles, detectableObjects, true, false));
		AddVectorObs(rayPer.TestPerceive(rayDistance, rayAngles, detectableObjects, false, true));
		AddVectorObs(transform.localPosition.x);
		AddVectorObs(transform.localPosition.y);
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int act = Mathf.FloorToInt(vectorAction[0]);
		switch (act)
		{
			case 1:
				// move up
				if (gridMoveDir != Vector2.down)
					gridMoveDir = Vector2.up;
				break;
			case 2:
				// move down
				if (gridMoveDir != Vector2.up)
					gridMoveDir = Vector2.down;
				break;
			case 3:
				// move left
				if (gridMoveDir != Vector2.right)
					gridMoveDir = Vector2.left;
				break;
			case 4:
				// move right
				if (gridMoveDir != Vector2.left)
					gridMoveDir = Vector2.right;
				break;
			default:
				Debug.Log("In act default");
				// penalize for not moving
				AddReward(-.00001f);
				break;
		}

	}

	/***
	 * Save current position, then move head object to a new position.
	 * Check if there is an attached tail, if so:
	 * Move the last tail object to the previously created gap
	 * Insert into front of list and remove from back
	 ***/
	private void MoveAgent()
	{
		Vector2 headPosition = transform.position;

		// move head, creating a gap between itself and the attach body part
		rb.MovePosition((Vector2)transform.position + gridMoveDir);	
		
		
		// if the snake has eaten, insert a new element into the gap
		if (hasEaten)
		{
			Debug.Log("Creating new body part");
			GameObject bodyPart = Instantiate(bodyPrefab, prevHeadPos, Quaternion.identity);
			bodyTransforms.Insert(0, bodyPart.transform);
			hasEaten = false;
		}
		else if(bodyTransforms.Count > 0)
		{
			// move last body part to the gap
			bodyTransforms.Last().position = prevHeadPos;
			// Insert that body part into correct position in the list
			bodyTransforms.Insert(0, bodyTransforms.Last());
			// Remove that body part from the end of the list
			bodyTransforms.RemoveAt(bodyTransforms.Count - 1);
		}

		prevHeadPos = headPosition;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Apple") && !hasEaten)
		{
			hasEaten = true;
			Debug.Log("Ate an apple!");
			//_rewardMultiplier /= .75f;
			AddReward(.75f);
			score++;
			
			// reset food object to wait position
			collision.gameObject.transform.position = academy.poolWaitPosition;
			Monitor.Log("reward multiplier", rewardMultiplier.ToString("n6"));
		}	
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("wall") || collision.collider.CompareTag("Body"))
		{
			Debug.Log("Collided into " + collision.collider.gameObject.name);
			SetReward(-1f);
			Done();
			academy.AcademyReset();
		}
	}
}

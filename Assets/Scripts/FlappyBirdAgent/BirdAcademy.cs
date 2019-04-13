using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;
using UnityEngine.UI;

public class BirdAcademy : Academy
{
	public BirdAgent agent;
	public float worldScrollSpeed = -1.5f;
	public Text scoreText;

	[Header("For Column Pooling")]
	public float spawnRate = 4f;
	public int columnPoolSize = 5;
	public float columnYMin = -2f;
	public float columnYMax = 2f;
	public GameObject columnsPrefab;

	private float timeSinceLastSpawn;
	private float spawnXPos = 10f;
	private float reward = 0f;
	private int currentColumnIndex = 0;
	private Vector2 objectPoolPos = new Vector2(4f, -2f);
	private GameObject[] columnPool;
	private GoalObject[] goalObjects;
	private ScrollingObject[] scrollingObjects;
	private int currentScore, maxScore = 0;
	private bool hasScored;

	private void Start()
	{
		scrollingObjects = FindObjectsOfType<ScrollingObject>();
		timeSinceLastSpawn = 0f;

		columnPool = new GameObject[columnPoolSize];
		for (int i = 0; i < columnPoolSize; i++)
		{
			columnPool[i] = Instantiate(columnsPrefab, objectPoolPos, Quaternion.identity);
			columnPool[i].name = "Column + " + i;
		}
	}

	public GoalObject[] GetGoalObjects()
	{
		if (columnPool != null)
			goalObjects = FindObjectsOfType<GoalObject>();
		return goalObjects;	
	}

	public override void InitializeAcademy()
	{
		Monitor.SetActive(true);
	}

	public override void AcademyReset()
	{
		ResetEnvironment();
	}

	private void ResetEnvironment()
	{
		Debug.Log("Resetting environment");
		if(currentScore > maxScore)
		{
			maxScore = currentScore;
		}
		currentScore = 0;
		reward = 0;
		scoreText.text = "Current Score: " + currentScore + " | Max Score: " + maxScore;
		agent.transform.position = Vector2.zero;
		agent.transform.rotation = new Quaternion(0, 0, 0, 0);
		ResetColumns();
		ResetScrollingObjects();
	}

	void Update()
	{
		hasScored = false;
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= spawnRate)
		{
			timeSinceLastSpawn = 0f;
			float spawnYPos = UnityEngine.Random.Range(columnYMin, columnYMax);

			columnPool[currentColumnIndex].transform.position = new Vector2(spawnXPos, spawnYPos);
			currentColumnIndex++;

			if (currentColumnIndex >= columnPoolSize)
				currentColumnIndex = 0;
		}
	}

	private void ResetScrollingObjects()
	{
		foreach (ScrollingObject obj in scrollingObjects)
		{
			obj.ResetScrollingObject();
		}
	}

	private void ResetColumns()
	{
		for (int i = 0; i < columnPool.Length; i++)
		{
			columnPool[i].transform.position = objectPoolPos;
		}
	}

	public void Score()
	{
		if (!hasScored)
		{
			hasScored = true;
			 reward += .01f;
			if(reward < .25f)
			{
				reward += .25f;
			}
			Monitor.Log("Reward for passing thru goal", reward.ToString("n6"));
			agent.AddReward(reward);
			currentScore++;
			scoreText.text = "Current Score: " + currentScore + " | Max Score: " + maxScore;
		}

	}
}

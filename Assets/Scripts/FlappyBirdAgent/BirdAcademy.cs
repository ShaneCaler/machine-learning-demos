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
	private int currentColumnIndex = 0;
	private Vector2 objectPoolPos = new Vector2(4f, -2f);
	private GameObject[] columnPool;
	private GoalObject[] goalObjects;
	private ScrollingObject[] scrollingObjects;
	private int score = 0;
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
			//goalObjects[i] = columnPool[i].GetComponentInChildren<GoalObject>().gameObject;
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
		score = 0;
		scoreText.text = "Score: " + score;
		agent.transform.position = Vector2.zero;
		agent.transform.rotation = new Quaternion(0, 0, 0, 0);
		//DestroyColumns();
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
		//GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
		/*for (int i = 0; i < columns.Length; i++)
		{
			Destroy(columns[i]);
		}*/

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
			agent.AddReward(.75f);
			score++;
			scoreText.text = "Score: " + score;
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System;

public class SnakeAcademy : Academy
{
	[Header("For food spawning")]
	[SerializeField] private GameObject foodPrefab;
	[SerializeField] private float spawnRate = 5f;
	[SerializeField] private int poolSize = 5;

	[Header("Border Transforms")]
	[SerializeField] private Transform borderTop;
	[SerializeField] private Transform borderBottom;
	[SerializeField] private Transform borderLeft;
	[SerializeField] private Transform borderRight;

	[HideInInspector] public Vector3 poolWaitPosition = new Vector3(50f, 0f);

	private SnakeAgent snake;
	private GameObject[] foodPool;
	private float timeSinceLastSpawn = 0f;
	private int currentPoolIndex = 0;

	public override void InitializeAcademy()
	{
		Monitor.SetActive(true);
	}

	public override void AcademyReset()
	{
		for (int i = 0; i < foodPool.Length; i++)
		{
			foodPool[i].transform.position = poolWaitPosition;
		}
		currentPoolIndex = 0;
		timeSinceLastSpawn = 0f;
	}

	private void Start()
	{
		snake = FindObjectOfType<SnakeAgent>();
		timeSinceLastSpawn = spawnRate;
		foodPool = new GameObject[poolSize];
		for (int i = 0; i < poolSize; i++)
		{
			foodPool[i] = Instantiate(foodPrefab, poolWaitPosition, Quaternion.identity, transform);
		}
	}

	private void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= spawnRate)
		{
			timeSinceLastSpawn = 0;
			SpawnFood();
		}

		CheckIfOutOfBounds();
	}

	private void CheckIfOutOfBounds()
	{
		if (snake.transform.position.y >= borderTop.position.y ||
			snake.transform.position.y <= borderBottom.position.y ||
			snake.transform.position.x <= borderLeft.position.x ||
			snake.transform.position.x >= borderRight.position.x)
		{
			Debug.Log("Snake is out of bounds");
			snake.Done();
		}
	}

	private void SpawnFood()
	{
		// check if the current pool object is waiting
		if(foodPool[currentPoolIndex].transform.position == poolWaitPosition)
		{
			// if so, set random position within the range of the borders
			int xPos = Mathf.FloorToInt(UnityEngine.Random.Range(borderLeft.position.x + 1f, borderRight.position.x - 1f));
			int yPos = Mathf.FloorToInt(UnityEngine.Random.Range(borderBottom.position.y + 1f, borderTop.position.y - 1f));
			foodPool[currentPoolIndex].transform.position = new Vector2(xPos, yPos);
		}
		
		// increment pool index and check if it's larger than the pool size, if so - reset to 0.
		currentPoolIndex++;
		if (currentPoolIndex >= poolSize)
			currentPoolIndex = 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{

	public float spawnRate = 4f;
	public int columnPoolSize = 5;
	public float columnYMin = -2f;
	public float columnYMax = 2f;
	public GameObject columnsPrefab;

	private float timeSinceLastSpawn;
	private float spawnXPos = 10f;
	private int currentColumnIndex = 0;
	private GameObject[] columnPool;
	private Vector2 objectPoolPos = new Vector2(-15f, -25f);

    // Start is called before the first frame update
    void Start()
    {
		columnPool = new GameObject[columnPoolSize];
		for(int i = 0; i < columnPoolSize; i++)
		{
			columnPool[i] = Instantiate(columnsPrefab, objectPoolPos, Quaternion.identity);
		}
    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLastSpawn += Time.deltaTime;
		if(!GameControl.Instance.isGameOver && timeSinceLastSpawn >= spawnRate)
		{
			timeSinceLastSpawn = 0;

			float spawnYPos = Random.Range(columnYMin, columnYMax);
			columnPool[currentColumnIndex].transform.position = new Vector2(spawnXPos, spawnYPos);
			currentColumnIndex++;

			if (currentColumnIndex >= columnPoolSize)
				currentColumnIndex = 0;
		}
    }

	public void DestroyColumns()
	{
		GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
		for (int i = 0; i < columns.Length; i++)
		{ 
			Destroy(columns[i]);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
	public static GameControl Instance;

	public float worldScrollSpeed = -1.5f;
	public bool isGameOver = false;
	public Text scoreText;

	private int score = 0;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (isGameOver)
		{
			score = 0;
			isGameOver = false;
		}
    }

	public void Score()
	{
		if (isGameOver)
			return;

		score++;
		scoreText.text = "Score: " + score;
	}

	public void Die()
	{
		isGameOver = true;
	}
}

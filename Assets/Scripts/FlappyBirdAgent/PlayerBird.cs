using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBird : MonoBehaviour
{
	public float jumpForce = 200f;
	private Rigidbody2D rb;
	private Animator anim;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(0, jumpForce));
			anim.SetTrigger("Flap");
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		SceneManager.LoadScene(0);
	}
}

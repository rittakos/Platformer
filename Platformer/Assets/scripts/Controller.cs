using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
	public float jumpForce = 200;

	//public LayerMask Ground;
	//public Transform levelBottom;
	public bool lastLevel;

	

	public AudioSource dieAudioSource;
	public AudioSource winAudioSource;
	public AudioSource jumpAudioSource;

	private Rigidbody2D rigidbody;

	private Vector3 playerStartPos = new(0.0f, 5f);

	private bool right = true;
	private Vector3 v = Vector3.zero;

	private bool onGround;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
  //      Debug.Log("move");
  //      onGround = false;

		//Collider2D[] colliders = Physics2D.OverlapCircleAll(playerBottom.position, 0.2f, Ground);
		//for (int i = 0; i < colliders.Length; i++)
		//{
		//	if (colliders[i].gameObject.tag == "Level")
		//		onGround = true;
		//	if (colliders[i].gameObject.tag == "Border" || colliders[i].gameObject.tag == "Enemy")
		//	{
		//		dieAudioSource.Play();
		//		reset();
		//	}
		//	if (colliders[i].gameObject.tag == "Goal")
		//	{
		//		//winAudioSource.Play();
		//		end();
		//	}
		//}
	}

	void end(bool win)
    {
		if (win)
		{
			winAudioSource.Play();
			SceneManager.LoadScene(sceneName: "Win");
		}
		else
		{
			dieAudioSource.Play();
			reset();
		}
	}

	public void reset()
    {
		rigidbody.position = playerStartPos;
	}

	public void move(float move, bool jump)
	{
        Vector3 targetVelocity = new Vector2(move * 10.0f, rigidbody.velocity.y);

		rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref v, 0.05f);


		if ((move > 0 && !right) || (move < 0 && right))
			turn();
			
		
		if (onGround && jump)
		{
			onGround = false;
			rigidbody.AddForce(new Vector2(0.0f, jumpForce));
			jumpAudioSource.Play();
		}
	}


	void turn()
	{
		right = !right;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		string tag = collision.gameObject.tag;
		Debug.Log(tag);

		if (tag == null) return;

		if(tag == "Ground")
			onGround = true;
		if (tag == "Border" || tag == "Enemy")
			end(false);
		if (tag == "Goal")
			end(true);
    }
}

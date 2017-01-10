using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follower : MonoBehaviour 
{
	public float followDistance = 2.0f;
	public float percentToDiePerSecond = 5;
	public float moveSpeed = 0.03f;
	public float inBaseMoveSpeed = 0.01f;
	public float MinAITimeIdle = 200;
	public float MaxAITimeIdle = 700;
	public AudioClip deathScream;

	float timeInTheDark;
	float currentMoveSpeed;
	float AITimeIdle = 400;
	float frameBasedTimer = 400;
	MainCharacterController targetToFollow = null;
	bool atBase = false;
	bool onTheMove = false;
	GameObject navLocation;


	void AISystem()
	{
		if(frameBasedTimer >= AITimeIdle)
		{
			var b = FindObjectOfType(typeof(Base)) as Base;
			// move toward a random AI point
			navLocation = b.GetNewAIPoint(navLocation);
			onTheMove = true;
			frameBasedTimer = 0;
		}
		if (onTheMove == true && Vector3.Distance(transform.position, navLocation.transform.position) < 0.1f)
		{
			onTheMove = false;
			frameBasedTimer = 0;
			AITimeIdle = Random.Range(MinAITimeIdle, MaxAITimeIdle);
			gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.IDLE);
		}
		else if (onTheMove == true)
		{
			float y = transform.position.y;
			float previZ = transform.position.z;
			var nav = navLocation.transform.position;
			var navMove = Vector3.MoveTowards(transform.position, nav, currentMoveSpeed);

			ChoseProperAnimation(nav, navMove);

			navMove.y = y;
			transform.position = navMove;
		}
		else
		{
			frameBasedTimer++;
		}
	}

	void Follow()
	{
		float y = transform.position.y;
		
		var pos = targetToFollow.transform.position;
		if (Vector3.Distance(pos, transform.position) > followDistance)
		{
			var newPos = Vector3.MoveTowards(transform.position, pos, currentMoveSpeed);

			ChoseProperAnimation(pos, newPos);

			newPos.y = y;
			transform.position = newPos;
		}
		if (Vector3.Distance(pos, transform.position) > 7.0f && Time.time - timeInTheDark > 1)
		{
			if (Random.Range(0f, 100f) < percentToDiePerSecond)
			{
				renderer.enabled = false;
				audio.PlayOneShot(deathScream);
				GameObject.Destroy(gameObject, 3);
			}
		}
	}

	private void ChoseProperAnimation(Vector3 targetPos, Vector3 newPos)
	{
		float prevX = transform.position.x;
		float prevZ = transform.position.z;
		if (Vector3.Distance(transform.position, newPos) > 0.01f)
		{
			if (Mathf.Abs(prevX - newPos.x) > Mathf.Abs(prevZ - newPos.z))
			{
				if (prevX < newPos.x)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.RIGHT);
				else if (prevX > newPos.x)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.LEFT);
			}
			else
			{
				if (prevZ < newPos.z)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.UP);
				else if (prevZ > newPos.z)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.DOWN);
			}
		}
		else
			gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.IDLE);

	}
	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
	}
	void FixedUpdate()
	{
		if (atBase)
		{
			currentMoveSpeed = inBaseMoveSpeed;
		}
		else
		{
			currentMoveSpeed = moveSpeed;
		}

		if (targetToFollow != null)
		{
			Follow();
		}
		else if (atBase)
		{
			AISystem();
		}
	}

	public void FollowMe(MainCharacterController controller)
	{
		targetToFollow = controller;
		timeInTheDark = Time.time;
	}

	void OnTriggerEnter(Collider other)
	{		
		var baseCollision = other.GetComponent<Base>();
		if (baseCollision != null)
		{
			// TODO: stop from following character and move around
			//          the base normally
			targetToFollow = null;
			atBase = true;
			if (!baseCollision.followers.Contains(this))
			{
				baseCollision.followers.Add(this);
			}
		}
		else
		{
			if (targetToFollow == null && atBase == false)
			{
				var character = other.GetComponent<MainCharacterController>();
				if (character != null)
					character.StartInteract(this);
			}
		}

	}
	void OnTriggerExit(Collider other)
	{
		var baseCollision = other.GetComponent<Base>();
		if(baseCollision != null)
		{
			atBase = false;
		}
	}
}

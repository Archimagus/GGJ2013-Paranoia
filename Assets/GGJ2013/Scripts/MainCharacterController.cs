using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour 
{
	public float MoveSpeed = 0.25f;
	public float Sanity = 100;
	public float MaxSanity = 100;
	public float InsanityPerSecond = 0.1f;
	public float SanityRecoveryPerSecond = 5;
	public float SanityPerFollower = 5;

	public float MinHeartRate = 60;
	public float MaxHeartRate = 240;
	public AudioClip[] heartBeats;
	public AudioClip gameOverScream;
	public int heartBeatIndex;

	public float CurrentHeartRate { get; private set; }

	bool gameOver = false;
	bool atBase = false;
	Vector3 moveDirection = new Vector3();
	float timer = 0;
	float blackScreenTimer = 0;
	float StartTime;
	float CurrentTime;
	float lastHeartbeatTime = 0;
	float initialMaxSanity;

	void GameOverWait()
	{
		blackScreenTimer = (float)((CurrentTime - StartTime));
		if (blackScreenTimer >= 6.0f)
		{
			Application.LoadLevel(0);
		}
	}
	// Use this for initialization
	void Start () 
	{
		initialMaxSanity = Sanity = MaxSanity;
		StartTime = Time.time;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CurrentTime = Time.time;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			var ps = FindObjectOfType(typeof(PauseScren)) as PauseScren;
			ps.Paused = !ps.Paused;
		}
		timer = (float)((CurrentTime-StartTime));
		if (gameOver == true)
		{
			GameOverWait();
			return;
		}

		moveDirection = Vector3.zero;
		moveDirection += Input.GetAxis("Horizontal") * Vector3.right * MoveSpeed;
		moveDirection += Input.GetAxis("Vertical") * Vector3.forward * MoveSpeed;
		if(moveDirection != Vector3.zero)
		{
			var xMag = Mathf.Abs(moveDirection.x);
			var zMag = Mathf.Abs(moveDirection.z);
			if (xMag > zMag)
			{
				if (moveDirection.x < 0)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.LEFT);
				else if (moveDirection.x > 0)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.RIGHT);
			}
			else
			{
				if (moveDirection.z < 0)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.DOWN);
				else if (moveDirection.z > 0)
					gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.UP);
			}
		}
		else
		{
			// NOTE: should also check for interactions
			gameObject.GetComponent<AnimationManager>().SetAnimation(AnimationManager.AnimationTypes.IDLE);
		}
		if(timer >= 1.0f)
		{
			timer = 0;
			StartTime = CurrentTime;
			if (true == atBase)
			{
				var b = FindObjectOfType(typeof(Base)) as Base;
				MaxSanity = initialMaxSanity + b.followers.Count * SanityPerFollower;
				if (Sanity < MaxSanity)
				{
					Sanity += SanityRecoveryPerSecond;
					Sanity = Mathf.Clamp(Sanity, 0, MaxSanity);
				}
			}
			else if (false == atBase && Sanity > 0)
			{
				Sanity -= InsanityPerSecond;
				if (Sanity <= 0)
				{
					// GAME OVER!!!!!!!!!!!!!!!!!!!!
					// SCREECH AND FADE TO BLACK MWAHAHAHAHA
					StartTime = Time.time;
					var lightObjects = FindObjectsOfType(typeof(Light));
					foreach (Light lightObject in lightObjects)
					{
						if (lightObject != null)
							lightObject.enabled = false;
					}
					gameOver = true;
					audio.PlayOneShot(gameOverScream);
				}
			}
		}

		var heartRateScale = Mathf.InverseLerp(MaxSanity, 0, Sanity);
		CurrentHeartRate = Mathf.Lerp(MinHeartRate, MaxHeartRate, heartRateScale);
		if (Time.time - lastHeartbeatTime > 60/CurrentHeartRate)
		{
			lastHeartbeatTime = Time.time;
			if (!heartBeats.IsNullOrEmpty() && Sanity > 0)
			{
				heartBeatIndex = (int)((heartBeats.Length) * heartRateScale);
				audio.PlayOneShot(heartBeats[heartBeatIndex]);
			}
		}
	}

	void FixedUpdate()
	{
		transform.position += moveDirection;
	}
	
	public void StartInteract(Follower follower)
	{
		follower.FollowMe(this);
	}
	
	public void OnTriggerEnter(Collider other)
	{
		var baseCollision = other.GetComponent<Base>();
		if (baseCollision != null)
			atBase = true;
	}
	public void OnTriggerExit(Collider other)
	{
		var baseCollision = other.GetComponent<Base>();
		if (baseCollision != null)
			atBase = false;
	}
}

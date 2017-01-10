using UnityEngine;
using System.Collections;

public class ScareZone : MonoBehaviour 
{
	public float SanityLossPerSecond = 1;

	float enterTime;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	void OnTriggerEnter(Collider other)
	{
		var player = other.GetComponent<MainCharacterController>();
		if (player != null)
		{
			enterTime = Time.time;
		}
	}
	void OnTriggerStay(Collider other)
	{
		var player = other.GetComponent<MainCharacterController>();
		if (player != null)
		{
			if (Time.time - enterTime > 1.0f)
			{
				player.Sanity -= SanityLossPerSecond;
				enterTime = Time.time;
			}
		}
	}
}

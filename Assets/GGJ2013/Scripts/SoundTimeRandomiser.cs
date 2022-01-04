using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundTimeRandomiser : MonoBehaviour 
{
	public float minPitch = 0.5f;
	public float maxPitch = 1f;
	public float minTime = 1.0f;
	public float maxTime = 5.0f;

	float nextTIme;
	float startTime;
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		nextTIme = Random.Range(minTime, nextTIme);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - startTime > nextTIme)
		{
			GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
			GetComponent<AudioSource>().Play();
			nextTIme = Random.Range(minTime, maxTime);
			startTime = Time.time;
		}
	}
}

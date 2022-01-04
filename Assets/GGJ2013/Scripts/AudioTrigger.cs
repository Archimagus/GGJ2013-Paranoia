using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(SphereCollider))]
public class AudioTrigger : MonoBehaviour 
{
	public bool playOnce = true;
	// Use this for initialization
	void Start () 
	{
		GetComponent<SphereCollider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	void OnTriggerEnter(Collider other)
	{
		GetComponent<AudioSource>().Play();
		if (playOnce)
		{
			GameObject.Destroy(this);
		}
	}
}

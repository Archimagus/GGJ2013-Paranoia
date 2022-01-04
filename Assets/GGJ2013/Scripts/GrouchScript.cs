using UnityEngine;
using System.Collections;

public class GrouchScript : MonoBehaviour 
{
	AnimationManager anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<AnimationManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider other)
	{
		GetComponent<AudioSource>().Play();
		anim.SetAnimation(AnimationManager.AnimationTypes.SITTING);
	}
}

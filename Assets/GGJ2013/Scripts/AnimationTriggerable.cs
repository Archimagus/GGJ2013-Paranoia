using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation), typeof(AudioSource))]
public class AnimationTriggerable : Triggerable 
{

	public string animationName;
	public AudioClip soundEffect;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void OnTriggerActivated(TriggerEvenArgs e)
	{
		if (!string.IsNullOrEmpty(animationName))
		{
			animation.Play(animationName);
		}
		else
		{
			animation.Play();
		}
		if (soundEffect != null)
		{
			audio.PlayOneShot(soundEffect);
		}
		enabled = false;
		e.T.enabled = false;
	}
}

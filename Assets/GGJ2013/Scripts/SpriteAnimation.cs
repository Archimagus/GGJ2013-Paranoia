using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour 
{
	public string label;
	public float FPS;
	private float secondsToWait;
	private float timer;
	private float currentTime;
	public bool loop;
	public Texture[] frames;
	private float startTime = 0;
	private int currentFrame;
	// Use this for initialization
	void Start () 
	{
		currentFrame = 0;
		secondsToWait = 1 / FPS;
		startTime = Time.time;
	}
	public bool UpdateFrames()
	{
		currentTime = Time.time;
		timer = (float)((currentTime-startTime));
		if(timer >= secondsToWait)
		{
			if (currentFrame >= frames.Length)
			{
				if (loop)
					currentFrame = 0;
				else
					return false;
			}
			renderer.material.mainTexture = frames[currentFrame];
			currentFrame++;
			startTime = Time.time;
		}
		return true;
	}
	public void ResetFrames()
	{
		currentFrame = 0;
	}
}

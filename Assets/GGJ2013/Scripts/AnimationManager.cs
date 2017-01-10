using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *                              -----  READ ME  ----
 *                          Initialize animations as the following:
 *                              animations[0] = IDLE animation
 *                              animations[1] = SITTING animation
 *                              animations[2] = RIGHT animation
 *                              animations[3] = LEFT animation
 *                              animations[4] = UP animation
 *                              animations[5] = DOWN animation
 * 								animations[6] = INTERACT animation
 */

public class AnimationManager : MonoBehaviour {
	public enum AnimationTypes { IDLE = 0, SITTING, RIGHT, LEFT, UP, DOWN, INTERACT };
	public SpriteAnimation[] animations;
	public AnimationTypes defaultAnimation = AnimationTypes.IDLE;
	AnimationTypes currentAnimation = AnimationTypes.IDLE;
	
	// Use this for initialization
	void Start ()
	{
		SetAnimation(defaultAnimation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!animations[(int)currentAnimation].UpdateFrames())
		{
			SetAnimation(defaultAnimation);
		}
	}
	public void SetAnimation(AnimationTypes _animation)
	{
		if (currentAnimation == _animation)
			return;
		animations[(int)currentAnimation].ResetFrames();
		currentAnimation = _animation;
	}
}

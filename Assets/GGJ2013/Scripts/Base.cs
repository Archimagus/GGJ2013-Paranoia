using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour 
{
	int maxFollowers=-1;
	public List<Follower> followers = new List<Follower>();
	public List<GameObject> AIPoints { get; set; }
	
	// Use this for initialization
	IEnumerator Start () 
	{
		AIPoints = new List<GameObject>();
		var points = GameObject.FindGameObjectsWithTag("AIPoints");
		foreach (var p in points)
		{
			AIPoints.Add(p);
		}
		yield return null;
		maxFollowers = FindObjectsOfType<Follower>().Length;
	}
	public GameObject GetNewAIPoint(GameObject oldPoint)
	{
		var ind = Random.Range(0, AIPoints.Count);
		var p = AIPoints[ind];
		AIPoints.Remove(p);
		if (oldPoint != null)
		{
			AIPoints.Add(oldPoint);
		}
		return p;
	}

	// Update is called once per frame
	void Update()
	{
		if(followers.Count == maxFollowers)
		{
			// Game Over.
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}

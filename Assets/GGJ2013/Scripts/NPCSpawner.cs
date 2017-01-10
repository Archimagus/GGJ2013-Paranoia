using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour 
{
	public GameObject[] availableCharacters;

	// Use this for initialization
	void Start () 
	{
		GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoints");
		if (!points.IsNullOrEmpty())
		{
			points.Shuffle();
			for (int i = 0; i < availableCharacters.Length; i++)
			{
				GameObject.Instantiate(availableCharacters[i], points[i].transform.position, points[i].transform.rotation);
			}
		}
	}

	// Update is called once per frame
	void Update() 
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour 
{
	public GameObject maleCharacter;
	public GameObject femaleCharacter;

	// Use this for initialization
	void Start () 
	{
		var charChoice = PlayerPrefs.GetString("CharacterChoice");
		if (string.IsNullOrEmpty(charChoice) || charChoice == "Female")
		{
			GameObject.Instantiate(femaleCharacter, transform.position, transform.rotation);
		}
		else
		{
			GameObject.Instantiate(maleCharacter, transform.position, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
